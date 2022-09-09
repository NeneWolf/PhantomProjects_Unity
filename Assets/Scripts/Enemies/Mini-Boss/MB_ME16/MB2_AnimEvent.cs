using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB2_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void MB2Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void M2RangeAttack() 
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void MB2Died()
    {
        StopAllEffects();
        m_audio[2].Play();
    }

    void MB2PlayerDetected()
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
