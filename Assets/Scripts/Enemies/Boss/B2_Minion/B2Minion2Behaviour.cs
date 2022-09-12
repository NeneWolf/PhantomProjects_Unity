using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

public class B2Minion2Behaviour : MonoBehaviour
{

    [SerializeField] AudioSource m_audio;

    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }
    [SerializeField] ParticleSystem bloodEffect;

    [Header("Healing")]
    [SerializeField] GameObject boss;
    [SerializeField] ParticleSystem particles;
    [SerializeField] float healAmount;

    // Difficulty
    DifficultyManager difficultyManager;
    public float difficulty;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    private void Start()
    {
        difficultyManager = GameObject.Find("DifficultyManager").gameObject.GetComponent<DifficultyManager>();
        difficulty = difficultyManager.difficultyMultiplier;
    }

    public void healBoss()
    {
        if (!isDead)
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

        if(difficulty > 1)
            boss.GetComponent<Entity>().Heal(healAmount * (difficulty/2));
        else
            boss.GetComponent<Entity>().Heal(healAmount);
    }

    public void TakeDamage(float dmg)
    {
        m_audio.Play();
        
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
