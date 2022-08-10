using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Managers
{
    public class ButtonManager : MonoBehaviour
    {
        // 0 - Main Menu //1 - Select Slot // 2 - Mode
        [SerializeField] GameObject[] menus; 

        //Background Effect
        [SerializeField] GameObject pCapsule;

        int difficultyLevel;

        public void MoveToSelectMenu()
        {
            menus[1].SetActive(true);
            menus[0].SetActive(false);
            pCapsule.GetComponent<Capsule>().TurnOnFog();
        }

        public void MoveToModeMenu()
        {
            menus[2].SetActive(true);
            menus[1].SetActive(false);
        }

        public void DisableDifficultyMenu(int difficultyLevel)
        {
            this.difficultyLevel = difficultyLevel;
            menus[2].SetActive(false);
            StartCoroutine(TheBreak());
        }

        public void ReturnToMainMenu()
        {
            pCapsule.GetComponent<Capsule>().TurnOffFog();

            foreach (var menu in menus)
            {
                menu.SetActive(false);
            }

            menus[0].SetActive(true);
        }

        void DifficultySelected(int difficultyLevel)
        {
            FindObjectOfType<DifficultyManager>().SetDifficulty(difficultyLevel);
            FindObjectOfType<ScenesManager>().BringNextScene("CharacterSelection");
        }

        IEnumerator TheBreak()
        {
            FindObjectOfType<Capsule>().PlayAnimationCrack();
            yield return new WaitForSeconds(2f);
            DifficultySelected(difficultyLevel);
        }
    }
}
