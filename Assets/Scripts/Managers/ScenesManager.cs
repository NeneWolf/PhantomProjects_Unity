using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PhantomProjects.Managers
{
    public class ScenesManager : MonoBehaviour
    {
        GameObject m_audioManager;
        GameObject dataManager;
        GameObject savingManager;

        public int currentScene;

        int sceneNumber;
        string sceneName;

        float waitingToSwitchScene = 2f;

       [SerializeField] GameObject fader;

        GameObject player;
        GameManager gameManager;


        private void Awake()
        {
            m_audioManager = FindObjectOfType<AudioManager>().gameObject;
            gameManager = FindObjectOfType<GameManager>();
            savingManager = GameObject.Find("SavingManager");
            dataManager = GameObject.Find("SavingManager");


        }

        //private void Start()
        //{
        //    print("Start Scene manager");
        //    currentScene = SceneManager.GetActiveScene().buildIndex;
        //    OnLevelWasLoaded(currentScene);
        //}

        //private void OnLevelWasLoaded(int level)
        //{
        //    if(level == 6)
        //    {
        //        print("Scene Manager Save");
        //        savingManager.GetComponent<DataPersistanceManager>().SaveGame();
        //    }
        //}

        private void Update()
        {
            currentScene = SceneManager.GetActiveScene().buildIndex;
            
            if (fader == null)
            {
                fader = findChildFromParent("Canvas", "Fader");
            }

            if (/*fader.GetComponent<Fader>().isLoading == false &&*/ m_audioManager.GetComponent<AudioManager>().IsPlayingBackground() == false)
            {
                PlayBackgroundSound(currentScene);
            }
           /* else if (fader.GetComponent<Fader>().isLoading == true)
                m_audioManager.GetComponent<AudioManager>().StopAllSounds();*/

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

        private void PlayBackgroundSound(int currentScene)
        {
            if (currentScene >= 0 && currentScene <= 3)
                CheckMusic("Main");
            else if (currentScene >= 6 && currentScene <= 10)          // Lab Scenes
                CheckMusic("Lab");
            else if (currentScene >= 12 && currentScene <= 16)          // Office Scenes
                CheckMusic("Office");
            else if (currentScene >= 18 && currentScene <= 22)         // Green House
                CheckMusic("GreenHouse");
            else if (currentScene == 11 || currentScene == 17)
                CheckMusic("BossRoom");                  // Boss rooms
            else if (currentScene == 4)
                CheckMusic("Victory");                   // Victory Screen // Completed game
            else if (currentScene == 5)
                CheckMusic("EndGame");                   // Player Died

        }

        void CheckMusic(string backMusic)
        { 
            //Check current sound player and if its the same as requested, stop it and play the correct one
            if (m_audioManager.GetComponent<AudioManager>().ReturnCurrentBackgroundMusicPlaying() != backMusic)
            {
                m_audioManager.GetComponent<AudioManager>().StopAllBackgroundSounds();
                m_audioManager.GetComponent<AudioManager>().PlaySound(backMusic);
            }
            else if(m_audioManager.GetComponent<AudioManager>().ReturnCurrentBackgroundMusicPlaying() == null)
            {
                m_audioManager.GetComponent<AudioManager>().PlaySound(backMusic);
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

