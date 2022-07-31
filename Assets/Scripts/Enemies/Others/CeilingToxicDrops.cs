using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingToxicDrops : MonoBehaviour
{
    [SerializeField] float damagePerDrop;

    private void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerStats>().TakeDamage(damagePerDrop);
        }
    }
}
