using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B1_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void B1Footsteps()
    {
        StopAllEffects();
        m_audio[3].Play();
    }

    void B1MeleeAttack()
    {
        StopAllEffects();
        m_audio[4].Play();
    }

    void B1Died()
    {
        StopAllEffects();
        m_audio[5].Play();
    }

    void B1PlayerDetected()
    {
        StopAllEffects();
        var count = Random.Range(0, 2);
        m_audio[count].Play();
    }

    void StopAllEffects()
    {
        foreach (AudioSource audio in m_audio)
        {
            audio.Stop();
        }
    }
}
