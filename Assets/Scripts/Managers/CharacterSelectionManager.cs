using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhantomProjects.Managers
{
    public class CharacterSelectionManager : MonoBehaviour
    {
        public GameObject spawnPosition;
        public GameObject[] characters;

        public Sprite[] sprite;
        public Image image;

        int indexSelected;

        int index;

        public void CharacterSelection()
        {
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
            FindObjectOfType<GameManager>().charactersIndex = indexSelected;
        }
    }
}
