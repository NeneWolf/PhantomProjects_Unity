using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomProjects.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] int level0Index = 6;
        int currentLevelIndex;

        //Player
        GameObject player;
        int playerIndex;
        public Sprite[] characterSprite;

        GameObject boss;
        CanvasUI canvas;

        public bool isBossLevel { get; private set; } = false;

        public int currentKeycards { get; private set; } = 0;
        public int currentMutationPoints { get; private set; } = 0;

        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void Update()
        {
            currentLevelIndex = FindObjectOfType<ScenesManager>().currentScene;

            //Fetch player and other ui information after the current scene index is 6 or above ( Level 0 > )
            if (currentLevelIndex >= level0Index)
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

            canvas.updateCanvasUI(player, boss, characterSprite[playerIndex]);
            canvas.UpdatePlayerUI(currentKeycards, currentMutationPoints);
        }

        public void KeyCardCollection()
        {
            currentKeycards += 1;
        }

        public void MutationPointsCollection(int points)
        {
            currentMutationPoints += points;
        }

    }
}
