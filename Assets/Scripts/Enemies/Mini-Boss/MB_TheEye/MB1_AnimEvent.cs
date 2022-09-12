using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MB1_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void B1Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void B1Attack()
    {
        StopAllEffects();
        m_audio[1].Play();
    }

    void B1Died()
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
