using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.cygnusprojects.TalentTree
{
    [AddComponentMenu("Cygnus Projects/Talentus Engine")]
    public class TalentusEngine : MonoBehaviour 
	{
        #region Variables
        public TalentTreeGraph TalentTree;
        public int AvailableSkillPoints = 0;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Do some initializing.
        /// In case a talenttree is specified, clean it up, assign the available skill points and evaluate the tree.
        /// </summary>
        public virtual void Start () 
		{
            if (TalentTree != null)
            {
                TalentTree.CleanUp();
                TalentTree.PointsToAssign = AvailableSkillPoints;
                Evaluate();
            }
		}
        #endregion

        #region Implementation
        /// <summary>
        /// Evaluate the talent tree (which skills can be bought?).
        /// </summary>                	
        public virtual void Evaluate()
        {
            if (TalentTree != null)
            {
                TalentTree.Evaluate();
            }
        }

        /// <summary>
        /// Apply the selected buy operations towards the actual tree.
        /// </summary>
        public virtual void Apply()
        {            
            if (TalentTree != null)
            {                
                TalentTree.Apply();
            }
        }

        /// <summary>
        /// Undo the selected by operations of the talent tree
        /// </summary>
        public virtual void Revert()
        {
            if (TalentTree != null)
            {
                TalentTree.Revert();
            }
        }

        /// <summary>
        /// Buy a specific talent from the tree.
        /// </summary>
        /// <param name="talent">Talent you want to buy.</param>
        public virtual void BuyTalent(TalentTreeNodeBase talent)
        {
            talent.Buy();
            Evaluate();
        }

        /// <summary>
        /// Unbuy a specific talent from the tree (can only be done when not applied yet).
        /// </summary>
        /// <param name="talent">The talent you want to 'unbuy'.</param>
        public virtual void RevertTalent(TalentTreeNodeBase talent)
        {
            talent.Revert();
            Evaluate();
        }
        
        /// <summary>
        /// Prepare a string to be saved containing the actual statusses of all the talents within the tree.
        /// </summary>
        /// <returns>A string that can be used for saving.</returns>
        public string SaveToString()
        {
            string retValue = string.Empty;
            for (int i = 0; i < TalentTree.talents.Count; i++)
            {
                retValue = retValue + TalentTree.talents[i].Save();
            }
            return retValue;
        }

        /// <summary>
        /// Updated the talents statusses using a string.
        /// Evaluates afterwards so all dependencies are correctly updated.
        /// </summary>
        /// <param name="statuses">A string in a predefined format containing the statusses of all talents within the tree.</param>
        public void LoadFromString(string statuses)
        {            
            for (int i = 0; i < TalentTree.talents.Count; i++)
            {
                string name = "[" + TalentTree.talents[i].name;                
                int startpos = statuses.IndexOf(name) + 1;
                int stoppos = statuses.IndexOf("]", startpos + 1);
                string substring = statuses.Substring(startpos, stoppos - startpos);
                TalentTree.talents[i].Load(substring);                 
            }
            Evaluate();
        }
                
        #endregion
    }
}
