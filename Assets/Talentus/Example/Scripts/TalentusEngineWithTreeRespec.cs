using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace com.cygnusprojects.TalentTree
{
    public class TalentusEngineWithTreeRespec : TalentusEngine
    {
        #region Variables
        public Text AvailableRespecPointsUI;
        public Text AvailableSkillPointsUI;
        public string Filename = "Assets/Talentus/Example/Resources/SavedTalentTree.TT";
        public int AvailableRespecPoints;

        [HideInInspector]
        public List<TalentTreeNodeBase> BoughtTalents = new List<TalentTreeNodeBase>();

        private int boughtTalents = 0;
        #endregion

        #region Unity Methods
        // Use this for initialization
        public override void Start()
        {
            base.Start();
            // Your code here    
        }

        private void OnEnable()
        {            
            // TalentBought is triggered when the talents get applied
            TalentTree.TalentBought += TalentTree_TalentBought;
            // TalentSelected is triggered when you buy a talent but you still did not applied it (so it still can be reverted)
            TalentTree.TalentSelected += TalentTree_TalentSelected;
            // TalentReverted is triggered when you did buy a talent and does revert it back.
            TalentTree.TalentReverted += TalentTree_TalentReverted;
            // TalentUnBought is triggered when a talent that was bought and applied is rolled back. fi for respec'ing.
            TalentTree.TalentUnBought += TalentTree_TalentUnBought;
        }

        private void OnDisable()
        {
            // Clear the event listening
            TalentTree.TalentBought -= TalentTree_TalentBought;
            TalentTree.TalentSelected -= TalentTree_TalentSelected;
            TalentTree.TalentReverted -= TalentTree_TalentReverted;
            TalentTree.TalentUnBought -= TalentTree_TalentUnBought;
        }

        private void TalentTree_TalentReverted(object sender, TalentTreeGraph.TalentRevertedEventArgs e)
        {
            Debug.LogWarning(string.Format("Talent {0} reverted, added {1} back to the available skill points.", e.Talent.Name, e.Cost.Cost));
        }

        private void TalentTree_TalentSelected(object sender, TalentTreeGraph.TalentSelectedEventArgs e)
        {
            Debug.LogWarning(string.Format("Talent {0} selected.", e.Talent.Name));
        }

        /// <summary>
        /// A talent was bought (buy + apply)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TalentTree_TalentBought(object sender, TalentTreeGraph.TalentBoughtEventArgs e)
        {            
            if (!BoughtTalents.Contains(e.Talent))
            {
                Debug.LogWarning(string.Format("Talent {0} bought.", e.Talent.Name));

                boughtTalents++;

                // Add the talent to the cach of bought talents
                BoughtTalents.Add(e.Talent);

                // Example: grant one respec point for every 3 skills bought.
                if (boughtTalents % 3 == 0)
                {
                    boughtTalents = 0;
                    AvailableRespecPoints++;
                }
            }
        }

        /// <summary>
        /// In case we unbuy (respec) a talent, we add the skillpoints back to the available skill points.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TalentTree_TalentUnBought(object sender, TalentTreeGraph.TalentUnBoughtEventArgs e)
        {
            Debug.LogWarning(string.Format("Talent {0} unbought, cost {1}.", e.Talent.Name, e.Info.Cost));
            TalentTree.PointsToAssign = TalentTree.PointsToAssign + e.Info.Cost;
            AvailableSkillPointsUI.text = TalentTree.PointsToAssign.ToString().Trim();
        }

        /// <summary>
        /// Unity update event
        /// In this example used to update the available skill points within the UI 
        /// Pressing S will also save the current state of the tree to disk.
        /// Pressing L will load the state of the tree from disk.
        /// </summary>
        void Update()
        {
            if (AvailableSkillPointsUI != null)
                AvailableSkillPointsUI.text = TalentTree.PointsToAssign.ToString().Trim();
            if (AvailableRespecPointsUI != null)
                AvailableRespecPointsUI.text = AvailableRespecPoints.ToString().Trim();
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveGraph();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                EvaluateAllRespectConditions();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadGraph();
            }
            if (Input.GetKeyDown(KeyCode.R)) // Reset the tree to it's initial values
            {
                foreach (TalentTreeNodeBase talent in TalentTree.talents)
                {
                    talent.Reset();
                }
                // Don't forget to reevaluate the tree after you changed some data:
                TalentTree.Evaluate();

                /* Or you can just reset the Bought flag of the Talent
                foreach (TalentTreeNodeBase talent in TalentTree.talents)
                {
                    foreach (TalentTreeCost cost in talent.Cost)
                    {
                        cost.Bought = false;
                    }
                }
                TalentTree.Evaluate();*/
            }
        }
        #endregion

        #region Implementation
        // Demo method to show how you can update the UI on all previous bought talents.
        public void EvaluateAllRespectConditions()
        {
            // Anything cached?
            if (BoughtTalents.Count > 0)
            {                
                foreach (var item in BoughtTalents)
                {
                    // Check if the level > 0, it was bought
                    int costLevel = item.GetLevel(false);
                    if (costLevel > 0)
                    {
                        // Update the UI by rethrowing the bought event
                        TalentTreeGraph.TalentBoughtEventArgs args = new TalentTreeGraph.TalentBoughtEventArgs();
                        args.Talent = item;
                        args.Cost = item.Cost[costLevel - 1];
                        TalentTree.ThrowTalentBought(args);
                    }
                }
            }
        }

        public void SaveGraph()
        {
            string path = Filename;
            //Write the graph to a file
            StreamWriter writer = new StreamWriter(path, false);
            writer.WriteLine(SaveToString());
            writer.Close();

            // Player prefs
            //PlayerPrefs.SetString("MyTree", SaveToString());
        }

        public void LoadGraph()
        {            
            string path = Filename;
            StreamReader reader = new StreamReader(path);
            string statuses = reader.ReadToEnd();
            reader.Close();           
            LoadFromString(statuses);

            // Player prefs
            //string importedData = PlayerPrefs.GetString("MyTree");
            //LoadFromString(importedData);

            // May sure we do update the cached talents list
            UpdateCachedTalents();
        }

        /// <summary>
        /// Update the cache with all bought talents, handy to reset the cache on load.
        /// </summary>
        private void UpdateCachedTalents()
        {
            // Clear cache by wiping the list
            BoughtTalents = new List<TalentTreeNodeBase>();
            // Any talents in the tree?
            if (TalentTree.talents.Count > 0)
            {
                for (int i = 0; i < TalentTree.talents.Count; i++)
                {
                    // Determine the talent
                    TalentTreeNodeBase t = TalentTree.talents[i];
                    // and check if it was bought
                    int costLevel = t.GetLevel(false);
                    if (costLevel > 0)
                    {
                        // add to the cache
                        BoughtTalents.Add(t);
                        // and make sure we trigger the Bought events
                        TalentTreeGraph.TalentBoughtEventArgs args = new TalentTreeGraph.TalentBoughtEventArgs();
                        args.Talent = t;
                        args.Cost = t.Cost[costLevel - 1];
                        TalentTree.ThrowTalentBought(args);
                    }
                }
            }
        }
        #endregion
    }
}
