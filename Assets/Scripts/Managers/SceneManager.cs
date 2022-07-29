using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PhantomProjects.Manager
{
    public class SceneManager : MonoBehaviour
    {
        int scene;
        int mutationPoints;
        bool isDead;

        Scene currentScene;

        //To add later the fader
        //Fader fade;

        //TODO: Save missing data between levels
        // 
        //Player Stats - Health & Energy
        //Upgrade System - Unlocked and locked
        //CharacterSelected

        GameObject player;

        private void Update()
        {
            player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                //isDead = player.GetComponent<PlayerMovement>().IsPlayerDead;

                if (isDead == true)
                {
                    mutationPoints = 0;
                }
            }
        }

        public void BringNextSchene(int sceneNumber)
        {
            scene = sceneNumber;
            //StartCoroutine("LoadScene"); <- For the fader between levels
        }

        IEnumerator LoadScene()
        {
            DontDestroyOnLoad(this.gameObject);

            if (scene != currentScene.buildIndex)
            {
                //fade = GameObject.Find("/Core/Canvas/Fader/").GetComponent<Fader>();
                //fade.FadeInOn();
                //yield return new WaitForSeconds(2f);
                //SceneManager.LoadScene(scene, LoadSceneMode.Single);
                yield return null;
            }
        }

        //Save the Mutation Points during the hole game
        public void SaveMutationPoints(int points)
        {
            mutationPoints = points;
        }

        public int ReturnMutationPoints()
        {
            return mutationPoints;
        }
    }
}

