using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Core;

namespace PhantomProjects.Managers
{
    public class GameManager : MonoBehaviour, IDataPersistance
    {
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
        UIManager uiManager;
        DifficultyManager difficultyManager;
        ScenesManager scenesManager;

        private void Awake()
        { 
            uiManager = GetComponent<UIManager>();
            difficultyManager = GetComponent<DifficultyManager>();
            scenesManager = GetComponent<ScenesManager>();
        }

        private void Update()
        {
            if(currentSceneIndex == level0Index)
            {
                inStartLevel = true;
            }

            if (inStartLevel)
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                    PlayerDetails();
                }
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

            // Send the saved data into the respective managers
            difficultyManager.difficultyLevel = gameDifficulty;
            scenesManager.currentScene = currentSceneIndex;
            player.GetComponent<PlayerStats>().SetHealthEnergyOnLoad(playerCurrentHealth,playerCurrentEnergy);
            uiManager.currentMutationPoints = mutationPointsCollected;
        }

        public void SaveData(ref GameData data)
        {
            data.characterSelected = this.charactersIndex;

            data.modeSelected = this.gameDifficulty;

            data.currentLevelIndex = this.currentSceneIndex;

            data.currentHealth = this.playerCurrentHealth;
            data.currentEnergy = this.playerCurrentEnergy;
            data.mutationPoints = this.mutationPointsCollected;

        }
    }
}
