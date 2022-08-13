using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{
    [SerializeField] GameObject fireBall;
    [SerializeField] float interval; // between spawning the void and shooting the ball;


    void Awake()
    {
        StartCoroutine(ActivatetFireBall());
    }

    IEnumerator ActivatetFireBall()
    {
        yield return new WaitForSeconds(interval);
        fireBall.SetActive(true);
        StartCoroutine(KillPortal());
    }

    IEnumerator KillPortal()
    {
        yield return new WaitForSeconds(1.6f);
        Destroy(this.gameObject);
    }
}
