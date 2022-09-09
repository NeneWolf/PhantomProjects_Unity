using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_AnimEvent : MonoBehaviour
{
    private AudioManager m_audioManager;

    // Start is called before the first frame update
    void Start()
    {
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void E2Footsteps()
    {
        m_audioManager.PlaySound("Guard - Footsteps");
    }

    void E2Shoot()
    {
        m_audioManager.PlaySound("Guard - Shoot");
    }
}
