using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Core;

namespace PhantomProjects.Managers
{
    public class GameManager : MonoBehaviour, IDataPersistance
    {
        GameObject sceneManager;
        DifficultyManager difficultyManager;

        Transform spawnPlayerPosition;

        // Data To Save
        public GameObject[] Characters;
        public int charactersIndex; // Character Select Manager

        public int gameDifficulty; // DifficultyManager
        public float difficultyMultiplier; // DifficultyManager

        public int currentSceneIndex; //SceneManager

        public float playerCurrentHealth;
        public float playerCurrentEnergy; 

        public int mutationPointsCollected; // UIManager

        [SerializeField] int level0Index = 6;
        public bool inStartLevel { get; private set; } = false;

        // 
        public GameObject player;

        private void Awake()
        {
            sceneManager = GameObject.Find("SceneManager");
            difficultyManager = FindObjectOfType<DifficultyManager>();
        }

        private void Update()
        {
            if (sceneManager.GetComponent<ScenesManager>().currentScene >= level0Index)
            {
                inStartLevel = true;
            }

            if (sceneManager.GetComponent<ScenesManager>().currentScene == 6 && player == null)
            {
                spawnPlayerPosition = GameObject.Find("PlayerSpawnPosition").transform;
                GameObject.Instantiate(Characters[charactersIndex],spawnPlayerPosition.transform.position, Quaternion.identity);
                player = GameObject.FindGameObjectWithTag("Player");
                PlayerDetails();
            }

            if (inStartLevel)
            {
                PlayerDetails();
            }
        }

        void PlayerDetails()
        {
            playerCurrentHealth = player.GetComponent<PlayerStats>().currentHealth;
            playerCurrentEnergy = player.GetComponent<PlayerStats>().currentEnergy;
        }

        public void LoadData(GameData data)
        {
            this.charactersIndex = data.characterSelected;
            this.gameDifficulty = data.modeSelected;
            this.currentSceneIndex = data.currentLevelIndex;
            this.playerCurrentHealth = data.currentHealth;
            this.playerCurrentEnergy = data.currentEnergy;
            this.mutationPointsCollected = data.mutationPoints;
         }

        public void SaveData(GameData data)
        {
            data.characterSelected = this.charactersIndex;

            data.modeSelected = difficultyManager.difficultyLevel;

            data.currentLevelIndex = sceneManager.GetComponent<ScenesManager>().currentScene;

            data.currentHealth = this.playerCurrentHealth;
            data.currentEnergy = this.playerCurrentEnergy;
            data.mutationPoints = this.mutationPointsCollected;
        }
    }
}
