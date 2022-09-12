using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueBox;
    public Dialogue dialogue;

    //public void TriggerDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueBox.SetActive(true);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }
}
