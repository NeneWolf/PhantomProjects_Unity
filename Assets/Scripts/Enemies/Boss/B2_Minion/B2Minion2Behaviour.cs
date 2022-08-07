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

    public void TakeDamage(float dmg)
    {
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
