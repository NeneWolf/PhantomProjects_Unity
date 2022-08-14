using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Managers
{
    public class GameManager : MonoBehaviour
    {
        // Data To Save
        public GameObject[] Characters;
        public int charactersIndex;

        public int gameDifficulty; // ButtonManager
        public float difficultyMultiplier; // DifficultyManager

        public int currentSceneIndex; //SceneManager


        // 
        public GameObject player;

        private void Awake()
        { 
            
        }

        private void Update()
        {
            if(GameObject.FindGameObjectWithTag("Player") != null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
        }
    }
}
