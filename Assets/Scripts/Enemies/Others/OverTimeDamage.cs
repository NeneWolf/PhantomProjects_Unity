using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverTimeDamage : MonoBehaviour
{
    [Header("Active Wire (Default)")]
    [SerializeField] bool wire = true; //Default
    [SerializeField] bool canDamage = false;

    [Header("Toxic Area")]
    [SerializeField] bool toxicArea = false;

    [Header("Damage Overtime")]
    [SerializeField] float damage;
    [SerializeField] float damageRate;

    [Header("Other Details")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] ParticleSystem sparks;


    private BoxCollider2D coll;
    Collider2D damageHit;
    float nextTimeToDamage = 0f;

    DifficultyManager difficultyManager;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
        damage *= difficultyManager.difficultyMultiplier;
    }

    void Start()
    {
        if (wire && canDamage)
        {
            sparks.Play();
        }
    }

    void Update()
    {
        damageHit = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, whatIsPlayer);

        if (wire && canDamage)
        {
            if (damageHit)
            {
                if(Time.time > nextTimeToDamage)
                {
                    nextTimeToDamage = Time.time + damageRate;
                    damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                }
            }
        }

        if (toxicArea && damageHit)
        {
            if (Time.time > nextTimeToDamage)
            {
                nextTimeToDamage = Time.time + damageRate;
                damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }

}
