using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion3Behaviour : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }

    [Header("Shooting")]
    [SerializeField] GameObject boss;
    [SerializeField] Transform shootingPoint;
    [SerializeField] GameObject toxicSpit;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public void FireToxicSpit()
    {
        if (boss.GetComponent<Entity>().CheckPlayerInMinAgroRange() && !isDead)
        {
            GameObject.Instantiate(toxicSpit, shootingPoint.transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PBullet")
        {
            //TODO: Add Damage from the player
            //TakeDamage()
        }
    }

    void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
