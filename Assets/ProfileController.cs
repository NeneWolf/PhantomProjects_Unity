using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour
{
    int playerIndex;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        playerIndex = GameObject.Find("GameManager").GetComponent<GameManager>().charactersIndex;

        switch (playerIndex)
        {
            case 0: // Player 1
                animator.SetBool("PMan", true);
                break;
            case 1:
                animator.SetBool("PWoman", true);
                break;
            default:
                animator.SetBool("PMan", true);
                break;
        }
    }
}
