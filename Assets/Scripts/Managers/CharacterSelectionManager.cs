using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomProjects.Managers
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        GameManager gameManager;
        ButtonManager buttonManager;
        ScenesManager sceneManager;

        public GameObject spawnPosition;
        public GameObject[] characters;

        public Sprite[] sprite;
        public Image image;
        public GameObject button;
        int indexSelected;

        int index;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            buttonManager = FindObjectOfType<ButtonManager>();
            sceneManager = FindObjectOfType<ScenesManager>();

            button.SetActive(false);
        }

        public void CharacterSelection()
        {
            button.SetActive(true);

            if (spawnPosition.transform.childCount > 0)
            {
                foreach(Transform child in spawnPosition.transform)
                {
                    Destroy(child.gameObject);
                }
                GameObject.Instantiate(characters[index], spawnPosition.transform);
            }
            else
                GameObject.Instantiate(characters[index], spawnPosition.transform);

            indexSelected = index;
        }

        public void CharacterSelection(int i)
        {
            button.SetActive(true);

            if (spawnPosition.transform.childCount > 0)
            {
                foreach (Transform child in spawnPosition.transform)
                {
                    Destroy(child.gameObject);
                }
                GameObject.Instantiate(characters[i], spawnPosition.transform);
            }
            else
                GameObject.Instantiate(characters[i], spawnPosition.transform);

            indexSelected = i;
        }

        public void CharacterVersion(int index)
        {
            image.GetComponent<Image>().sprite = sprite[index];
            this.index = index;
        }

        public void ConfirmSelected()
        {
            gameManager.charactersIndex = indexSelected;
            buttonManager.OnNewGameClicked();
            sceneManager.BringNextScene("SampleScene");

        }
    }
}
