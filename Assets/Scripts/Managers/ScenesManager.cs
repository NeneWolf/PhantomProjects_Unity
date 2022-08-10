using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PhantomProjects.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        int sceneNumber;
        string sceneName;
        bool isDead;

        float waitingToSwitchScene = 2f;

        Scene currentScene;
        GameObject canvas;
       [SerializeField] GameObject fader;

        GameObject player;
        private void Awake()
        {
            fader = findChildFromParent("Canvas", "Fader");
        }

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                isDead = player.GetComponent<PlayerStats>().IsPlayerDead;
                BringNextScene("EndGame");
            }
        }

        GameObject findChildFromParent(string parentName, string childNameToFind)
        {
            string childLocation = "/" + parentName + "/" + childNameToFind;
            GameObject childObject = GameObject.Find(childLocation);
            return childObject;
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

