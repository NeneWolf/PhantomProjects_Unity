using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion3Behaviour : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }
    [SerializeField] ParticleSystem bloodEffect;

    [Header("Shooting")]
    [SerializeField] GameObject boss;
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject toxicSpit;



    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }


    public void FireToxicSpit()
    {
        if (!isDead)
        {
            GameObject.Instantiate(toxicSpit, shootingPoint.transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float dmg)
    {
        GameObject.Instantiate(bloodEffect, transform.position, Quaternion.identity);

        if (currentHealth - dmg <= 0)
        {
            currentHealth = 0;
            isDead = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            currentHealth -= dmg;
        }
    }
}
