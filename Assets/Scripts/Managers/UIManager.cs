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

        void  Update()
        {
            if (gameManager.inStartLevel && gameManager.player != null)
            {
                canvas = GameObject.Find("Canvas").GetComponent<CanvasUI>();
                levelUIManager();
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

    }
}
