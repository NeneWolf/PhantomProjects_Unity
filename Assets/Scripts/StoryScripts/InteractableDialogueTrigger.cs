using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDialogueTrigger : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject dialogueBox;
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            dialogueBox.SetActive(true);
            Time.timeScale = 0f;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactionText.SetActive(false);
        }
    }
}
