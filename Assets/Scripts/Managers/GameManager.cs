using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Core;
using UnityEngine.SceneManagement;

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

        public float shieldDuration;
        public float shieldCooldown;
        
        public float fireDelay;
        
        public bool loadedSave = false;

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
            else
                inStartLevel = false;

            if (player == null && inStartLevel)
            {
                spawnPlayerPosition = GameObject.Find("PlayerSpawnPosition").transform;
                GameObject.Instantiate(Characters[charactersIndex], spawnPlayerPosition.transform.position, Quaternion.identity);
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else if (player != null && inStartLevel)
            {
                if (fireDelay != 0)
                    player.GetComponentInChildren<PlayerWeapon>().fireDelay = fireDelay;

                if (shieldDuration != 0)
                    player.GetComponentInChildren<PlayerAbilities>().UpdateDuration(shieldDuration);

                if (shieldCooldown != 0)
                    player.GetComponentInChildren<PlayerAbilities>().UpdateCooldown(shieldCooldown);
            }
            else if( player != null && !inStartLevel)
            {
                Destroy(player);
            }

            //print(inStartLevel);
        }

        public void LoadData(GameData data)
        {
            this.loadedSave = data.loadedPlayer;
            this.charactersIndex = data.characterSelected;
            this.gameDifficulty = data.modeSelected;
            this.currentSceneIndex = data.currentLevelIndex;
            this.playerCurrentHealth = data.currentHealth;
            this.playerCurrentEnergy = data.currentEnergy;
            this.fireDelay = data.gunRate;
            this.shieldCooldown = data.shieldCooldown;
            this.shieldDuration = data.shieldDuration;
        }

        public void SaveData(GameData data)
        {
            loadedSave = true;

            data.loadedPlayer = this.loadedSave;

            data.characterSelected = this.charactersIndex;
            data.modeSelected = difficultyManager.difficultyLevel;
            data.modeSelected = gameDifficulty;
            

            if (player != null)
            {
                data.currentHealth = player.GetComponent<PlayerStats>().currentHealth;
                data.currentEnergy = player.GetComponent<PlayerStats>().currentEnergy;
                data.gunRate = player.GetComponentInChildren<PlayerWeapon>().fireDelay;
                data.shieldDuration = shieldDuration;
                data.shieldCooldown = shieldCooldown;
            }

            currentSceneIndex = sceneManager.GetComponent<ScenesManager>().currentScene;

            ////If current scene is level 0 save that as currentLevelIndex
            if (currentSceneIndex < level0Index)
            {
                data.currentLevelIndex = 1;
            }

            // if current scene is above level 0 save currentScene + 1
            if (currentSceneIndex >= level0Index)
            {
                data.currentLevelIndex = currentSceneIndex;
            }

        }

        public void RetrieveShieldData(float duration, float cooldown)
        {
            this.shieldDuration = duration;
            this.shieldCooldown = cooldown;
        }
    }
}
