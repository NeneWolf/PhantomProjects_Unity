using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PhantomProjects.Manager;

namespace PhantomProjects.Core
{
    public class CanvasUI : MonoBehaviour
    {

        // if it has red - It works, just visual studio refuses to bring the right files dw...
        [Header("UI Text Fields")]
        [Space]
        [SerializeField] TextMeshProUGUI keycardsTextDisplay;
        [SerializeField] TextMeshProUGUI mutationPointsTextDisplay;

        [Header("Keycards - Requirement")]
        [Space]
        [SerializeField] int requiredKeyNumber;

        int currentKeycards = 0;
        int currentMutationPoints = 0;

        //For Pause menu
        bool isPaused;

        //Player information
        GameObject player;
        bool isDead;

        GameObject sceneManager;

        private void Start()
        {
            currentMutationPoints = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().ReturnMutationPoints();
            player = GameObject.FindGameObjectWithTag("Player");
            sceneManager = GameObject.FindGameObjectWithTag("SceneManager");

            keycardsTextDisplay.text = currentKeycards.ToString();
            mutationPointsTextDisplay.text = currentMutationPoints.ToString();
        }

        private void Update()
        {
            if (player != null)
            {
                isDead = player.GetComponent<PlayerState>().IsPlayerDead;
                UpdateDisplay();
            }
        }

        void UpdateDisplay()
        {
            keycardsTextDisplay.text = currentKeycards.ToString();
            mutationPointsTextDisplay.text = currentMutationPoints.ToString();
        }


        public void KeyCardCollection()
        {
            currentKeycards += 1;
        }

        public void MutationPointsCollection(int points)
        {
            currentMutationPoints += points;
            sceneManager.GetComponent<SceneManager>().SaveMutationPoints(currentMutationPoints);
        }

        // To allow the player to move to the next Level if meet the requirements
        public bool ReportKeysCollection()
        {
            if (currentKeycards == requiredKeyNumber)
            {
                return true;
            }
            else
                return false;
        }
        
        // Add other functionalites for the menus
    }
}

