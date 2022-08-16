using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomProjects.Managers
{
    public class UIManager : MonoBehaviour
    {
        GameManager gameManager;
        GameObject boss;
        CanvasUI canvas;

        //Player
        GameObject player;
        int playerIndex;
        public Sprite[] characterSprite;

        public bool isBossLevel { get; private set; } = false;

        public int currentKeycards { get; private set; } = 0;
        public int currentMutationPoints = 0;

        

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            if (gameManager.inStartLevel)
            {
                canvas = GameObject.Find("/Core/Canvas").gameObject.GetComponent<CanvasUI>();
                levelUIManager();
            }
        }

        void levelUIManager()
        {
            playerIndex = GameObject.Find("GameManager").GetComponent<GameManager>().charactersIndex;
            player = gameManager.player;
            boss = GameObject.FindGameObjectWithTag("Boss");

            if (boss != null)
                isBossLevel = true;

            canvas.updateCanvasUI(player, boss, characterSprite[playerIndex], gameManager.inStartLevel);
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

    }
}
