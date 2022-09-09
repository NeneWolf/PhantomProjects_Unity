using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1M_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void B1MFootsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void B1MDied()
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void StopAllEffects()
    {
        foreach (AudioSource audio in m_audio)
        {
            audio.Stop();
        }
    }
}
