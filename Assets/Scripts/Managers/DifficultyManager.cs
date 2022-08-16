using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Managers 
{ 
    public class DifficultyManager : MonoBehaviour
    {
        [Header("Game Difficulty Setting")]
        // 0- easy // 1-normal // 2-hard
        public bool[] difficulty;

        public int difficultyLevel;
        public float difficultyMultiplier;

        private void Update()
        {
            FindObjectOfType<GameManager>().difficultyMultiplier = difficultyMultiplier;
            SetDifficultyMultiplier();
        }

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
        }

        void SetDifficultyMultiplier()
        {
            switch (difficultyLevel)
            {
                case 0:
                    difficultyMultiplier = 1f;
                    break;
                case 1:
                    difficultyMultiplier = 1.2f;
                    break;
                case 2:
                    difficultyMultiplier = 10f;
                    break;
                default:
                    difficultyMultiplier = 1f;
                    break;

            }
        }
    }
}
