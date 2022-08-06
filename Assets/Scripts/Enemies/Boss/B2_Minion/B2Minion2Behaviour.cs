using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion2Behaviour : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }

    [Header("Healing")]
    [SerializeField] GameObject boss;
    [SerializeField] ParticleSystem particles;
    [SerializeField] float healAmount;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    private void Update()
    {
        if(currentHealth <= 0)
        {
            isDead = true;
        }

        //TEST 

        if (Input.GetKeyDown(KeyCode.O))
        {
            boss.GetComponent<Entity>().Damage(50);
        }
    }

    public void healBoss()
    {
        if (boss.GetComponent<Entity>().CheckPlayerInMinAgroRange() && !isDead)
        {
            if(boss.GetComponent<Entity>().currentHealth < boss.GetComponent<Entity>().entityData.maxHealth)
            {
                particles.Play();
                StartCoroutine(HealStart());
            }
        }
    }

    IEnumerator HealStart()
    {
        yield return new WaitForSeconds(1f);
        boss.GetComponent<Entity>().Heal(healAmount);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PBullet")
        {
            //TODO:
            //TakeDamage();
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
