using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingToxicDrops : MonoBehaviour
{
    [SerializeField] float damagePerDrop;

    DifficultyManager difficulty;

    AudioManager m_audioManager;

    private void Start()
    {
        m_audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>().gameObject.GetComponent<AudioManager>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            difficulty = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
            damagePerDrop *= difficulty.difficultyMultiplier;
            other.GetComponent<PlayerStats>().TakeDamage(damagePerDrop);
        }

        if(other.tag == "Ground")
        {
            m_audioManager.PlaySound("Ceiling Drop");
        }
    }
}
