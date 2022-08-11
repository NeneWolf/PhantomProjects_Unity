using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.cygnusprojects.TalentTree
{
    public class TalentusAutoEnableRoots : MonoBehaviour
    {
        public TalentusEngine Engine;
        public string[] SkillRoots;

        // Use this for initialization
        void Start()
        {
            if (Engine != null && SkillRoots != null)
            {
                if (SkillRoots.Length > 0)
                {
                    //For each root talent defined
                    for (int i = 0; i < SkillRoots.Length; i++)
                    {
                        // Look up the talent in the tree
                        TalentTreeNodeBase talent = Engine.TalentTree.FindTalent(SkillRoots[i]);
                        // Did the talent exist?
                        if (talent != null)
                        {
                            // v.1.3.0 to change the Explanation and Cost description fields at runtime you can use this:
                            talent.Explanation = "New explanation";
                            talent.Cost[0].Description = "Description";

                            // We need to know the cost of the root so we can make sure we don't spend the skillpoints of the actual gamer.
                            TalentTreeNodeNextLevel cost2buy = talent.GetCostForNextLevel();
                            // Add the cost to the available skill points of the engine.
                            Engine.AvailableSkillPoints = Engine.AvailableSkillPoints + cost2buy.Cost;
                            // Buy the talent, note this will reduce the skillpoints so make sure you have at least the amount available
                            Engine.BuyTalent(talent);
                        }
                    }
                    // Make our selection permanent
                    Engine.Apply();               
                }
            }
        }

        
    }
}
