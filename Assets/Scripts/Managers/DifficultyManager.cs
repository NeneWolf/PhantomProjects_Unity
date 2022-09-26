using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Managers 
{ 
    public class DifficultyManager : MonoBehaviour,IDataPersistance
    {
        [Header("Game Difficulty Setting")]
        // 0- easy // 1-normal // 2-hard
        public bool[] difficulty;

        public int difficultyLevel;
        public float difficultyMultiplier;

        public void SetDifficulty(int number)
        {
            for(int i = 0; i < difficulty.Length; i++)
            { 
                if (i == number)
                {
                    difficulty.SetValue(true, number);
                    difficultyLevel = number;
                }
                else
                {
                    difficulty.SetValue(false, i);
                }
            }
            SetDifficultyMultiplier();
        }

        void SetDifficultyMultiplier()
        {
            switch (difficultyLevel)
            {
                case 0:
                    difficultyMultiplier = 0.5f;
                    break;
                case 1:
                    difficultyMultiplier = 1f;
                    break;
                case 2:
                    difficultyMultiplier = 2f;
                    break;
                default:
                    difficultyMultiplier = 1f;
                    break;

            }

            FindObjectOfType<GameManager>().difficultyMultiplier = difficultyMultiplier;
        }

        public void LoadData(GameData data)
        {
            this.difficultyLevel = data.modeSelected;
            SetDifficultyMultiplier();
        }

        public void SaveData(GameData data)
        {
            //throw new System.NotImplementedException();
        }
    }
}
