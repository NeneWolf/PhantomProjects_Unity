using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerAnimation : MonoBehaviour
{
    public Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        
        anim.Play();
    }
}
