using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.cygnusprojects.TalentTree
{
    public class TalentRespecScript : MonoBehaviour
    {
        public Button RespecButton;
        private TalentUI tUI;
        private TalentusEngineWithRespec engine;

        /// <summary>
        /// Make sure the variables are fetched and the events added
        /// </summary>
        private void OnEnable()
        {
            tUI = GetComponent<TalentUI>();
            if (RespecButton != null)
                RespecButton.gameObject.SetActive(false);

            engine = (TalentusEngineWithRespec)tUI.Engine;
            engine.TalentTree.TalentBought += TalentTree_TalentBought;
        }

        /// <summary>
        /// Clear the events
        /// </summary>
        private void OnDisable()
        {
            engine.TalentTree.TalentBought -= TalentTree_TalentBought;
        }

        /// <summary>
        /// A talent was bought, enable or disable the button depending if it would be safe or not to respec the skill.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TalentTree_TalentBought(object sender, TalentTreeGraph.TalentBoughtEventArgs e)
        {
            if (RespecButton != null)
            {
                TalentTreeNodeBase skill = tUI.Talent;
                if (e.Talent == skill)
                {                 
                    bool okToRespec = engine.TalentTree.CanBeUnbought(skill);
                    if (okToRespec)
                        RespecButton.gameObject.SetActive(true);
                    else
                        RespecButton.gameObject.SetActive(false);
                }           
            }
        }

        /// <summary>
        /// Perform the unbuying in case we have enough respec points.
        /// </summary>
        public void Respec()
        {            
            if (engine.AvailableRespecPoints > 0)
            {
                if (engine.TalentTree.UnBuy(tUI.Talent) != null)
                {
                    Debug.Log("Unbought");
                    RespecButton.gameObject.SetActive(false);

                    engine.AvailableRespecPoints--;
                    if (engine.AvailableRespecPoints < 0)
                        engine.AvailableRespecPoints = 0;
                }
            }
            else Debug.Log("No Respec points!");
        }
        
    }
}