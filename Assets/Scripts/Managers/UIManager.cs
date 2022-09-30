using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomProjects.Managers
{
    public class UIManager : MonoBehaviour, IDataPersistance
    {
        GameManager gameManager;
        CanvasUI canvas;

        //Player
        GameObject player;
        float playerHealth;
        int playerIndex;
        public Sprite[] characterSprite;

        public int currentKeycards { get; private set; } = 0;
        public int currentMutationPoints = 0;

        // Volume 
        public float effectVolume;
        public float backgroundVolume;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            if (gameManager.inStartLevel)
            {
                if (player == null)
                    player = GameObject.FindGameObjectWithTag("Player");
                
                canvas = GameObject.FindObjectOfType<CanvasUI>();

                if (canvas != null)
                {
                    levelUIManager();
                }
            }
        }

        void levelUIManager()
        {
            playerIndex = GameObject.Find("GameManager").GetComponent<GameManager>().charactersIndex;
            player = GameObject.FindGameObjectWithTag("Player");
            
            canvas.updateCanvasUI(characterSprite[playerIndex], gameManager.inStartLevel);
            canvas.UpdatePlayerUI(currentKeycards, currentMutationPoints);
        }

        public void KeyCardCollection()
        {
            currentKeycards += 1;
        }

        public void MutationPointsCollection(int points)
        {
            currentMutationPoints += points;
            gameManager.GetComponent<GameManager>().mutationPointsCollected = currentMutationPoints;
        }

        public void MutationPointsRemove(int points)
        {
            currentMutationPoints -= points;
            gameManager.GetComponent<GameManager>().mutationPointsCollected = currentMutationPoints;
        }

        public void LoadData(GameData data)
        {
            this.currentMutationPoints = data.mutationPoints;
        }

        public void SaveData(GameData data)
        {
            data.mutationPoints = this.currentMutationPoints;
        }

        public void ResetAllData()
        {
            currentKeycards = 0;
            currentMutationPoints = 0;
        }

        public void ResetKeyCards(){
            currentKeycards = 0;
        }
    }
}
