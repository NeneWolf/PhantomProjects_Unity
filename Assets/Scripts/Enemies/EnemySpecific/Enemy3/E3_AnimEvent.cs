using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_AnimEvent : MonoBehaviour
{
    [SerializeField] AudioSource[] m_audio;

    void E3Footsteps()
    {
        StopAllEffects();
        m_audio[0].Play();
    }

    void E3MeleeAttack()
    {
        //?
    }

    void E3Died()
    {
        // ??
    }

    void StopAllEffects()
    {
        foreach (AudioSource audio in m_audio)
        {
            audio.Stop();
        }
    }
}
