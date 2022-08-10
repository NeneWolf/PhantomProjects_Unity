using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [SerializeField] Sprite spriteOff;
    [SerializeField] Sprite spriteOn;
    [SerializeField] ParticleSystem[] fogParticles;

    bool canCrack = false;
    SpriteRenderer sprite;

    Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        sprite.sprite = spriteOff;
    }

    public void PlayAnimationCrack()
    {
        canCrack = true;
        animator.SetBool("canCrack", canCrack);
    }

    public void TurnOnFog()
    {
        sprite.sprite = spriteOn;

        foreach (var fog in fogParticles)
        {
            fog.Play();
        }
    }

    public void TurnOffFog()
    {
        sprite.sprite = spriteOff;

        foreach (var fog in fogParticles)
        {
            fog.Stop();
        }
    }
}
