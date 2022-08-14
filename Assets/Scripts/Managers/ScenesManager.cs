using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PhantomProjects.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        public int currentScene { get; private set; }

        int sceneNumber;
        string sceneName;

        float waitingToSwitchScene = 2f;

       [SerializeField] GameObject fader;

        GameObject player;
        GameManager gameManager;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            fader = findChildFromParent("Canvas", "Fader");
        }

        private void Update()
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;

            //Send Daddy Game Manager Info and retrieve
            gameManager.currentSceneIndex = currentScene;
            player = gameManager.player;

            if (player.GetComponent<PlayerStats>().IsPlayerDead)
            {
                BringNextScene("EndGame");
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

        GameObject findChildFromParent(string parentName, string childNameToFind)
        {
            string childLocation = "/" + parentName + "/" + childNameToFind;
            GameObject childObject = GameObject.Find(childLocation);
            return childObject;
        }

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
            fader.SetActive(true);
            fader.GetComponent<Fader>().FadeOutOn();
        }
    }
}

