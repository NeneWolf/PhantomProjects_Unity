using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    GameObject audioManager;

    public void PlaySound()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>().gameObject;
        audioManager.GetComponent<AudioManager>().PlaySound("ButtonClick");
    }
}
