using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PhantomProjects.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        GameObject dataManager;

        public int currentScene;

        int sceneNumber;
        string sceneName;

        float waitingToSwitchScene = 2f;

       [SerializeField] GameObject fader;

        GameObject player;
        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            dataManager = GameObject.Find("SavingManager");
        }

        private void Update()
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;

            if (fader == null)
            {
                fader = findChildFromParent("Canvas", "Fader");
            }

            if (gameManager.inStartLevel)
            {
                player = GameObject.FindGameObjectWithTag("Player");

                if(player != null)
                {
                    if (player.GetComponent<PlayerStats>().IsPlayerDead)
                    {
                        BringNextScene("EndGame");
                    }
                }
            }
        }

        //By Name
        public void BringNextScene(string name)
        {
            sceneName = name;
            StartCoroutine(LoadScenebyName());
        }

        //ByNumber
        public void BringNextSchene()
        {
            sceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadScenebyNumber());
        }

        public void ReturnToPreviousScene()
        {
            sceneNumber = SceneManager.GetActiveScene().buildIndex - 1;
            StartCoroutine(LoadScenebyNumber());
        }

        public void LoadScene(int sceneIndex)
        {
            sceneNumber = sceneIndex;
            StartCoroutine(LoadScenebyNumber());
        }

        GameObject findChildFromParent(string parentName, string childNameToFind)
        {
            string childLocation = "/" + parentName + "/" + childNameToFind;
            GameObject childObject = GameObject.Find(childLocation);
            return childObject;
        }

        //Fader
        IEnumerator LoadScenebyName()
        {
            FindFaderAndTurnOn();
            yield return new WaitForSeconds(waitingToSwitchScene);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            yield return null;
        }

        IEnumerator LoadScenebyNumber()
        {
            FindFaderAndTurnOn();
            yield return new WaitForSeconds(waitingToSwitchScene);
            SceneManager.LoadScene(sceneNumber, LoadSceneMode.Single);
            yield return null;
        }

        void FindFaderAndTurnOn()
        {
            if(fader != null)
            {
                fader.SetActive(true);
                fader.GetComponent<Fader>().FadeOutOn();
            }
        }
    }
}

