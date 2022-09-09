using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;


    void E1Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void E1Attack()
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void E1Died()
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
