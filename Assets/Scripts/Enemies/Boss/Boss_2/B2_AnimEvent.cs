using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void B2Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void B2MeleeAttack()
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void B2Dead()
    {
        StopAllEffects();
        m_audio[2].Play();
    }

    void B2PlayerDetected()
    {
        StopAllEffects();
        m_audio[3].Play();
    }

    void StopAllEffects()
    {
        foreach (AudioSource audio in m_audio)
        {
            audio.Stop();
        }
    }
}
