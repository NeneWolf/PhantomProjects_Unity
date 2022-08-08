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

    //public bool Test;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = spriteOff;
    }

    //private void Update()
    //{
    //    if (Test)
    //    {
    //        TurnOnFog();
    //    }
    //    else
    //        TurnOffFog();
    //}

    public void PlayAnimationCrack(bool On)
    {
        canCrack = On;
        //TODO : Add animation
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
