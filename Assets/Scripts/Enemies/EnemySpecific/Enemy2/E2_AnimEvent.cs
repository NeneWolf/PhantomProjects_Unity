using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;


    void E2Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void E2Shoot()
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void E2Died()
    {
        StopAllEffects();
        m_audio[2].Play();
    }

    void StopAllEffects()
    {
        foreach (AudioSource audio in m_audio)
        {
            audio.Stop();
        }
    }
}
