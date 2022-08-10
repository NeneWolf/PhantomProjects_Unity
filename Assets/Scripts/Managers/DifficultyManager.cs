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

        // Start is called before the first frame update
        void Start()
        {
            //Never Destroy this object
            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            
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
    }
}
