using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueBox;
    public Dialogue dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dialogueBox.SetActive(true);
            Time.timeScale = 0f;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Destroy(this.gameObject);
        }
    }
}