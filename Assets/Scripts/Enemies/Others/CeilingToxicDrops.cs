using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingToxicDrops : MonoBehaviour
{
    [SerializeField] float damagePerDrop;

    DifficultyManager difficulty;
    

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            difficulty = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
            damagePerDrop *= difficulty.difficultyMultiplier;
            other.GetComponent<PlayerStats>().TakeDamage(damagePerDrop);
        }
    }
}
