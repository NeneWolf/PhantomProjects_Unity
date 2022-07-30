using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireBehaviour : MonoBehaviour
{
    [Header("Active Wire")]
    [SerializeField] bool canDamage = false;
    [SerializeField] float damage;
    [SerializeField] float damageRate;

    [Header("Other Details")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] ParticleSystem sparks;


    private BoxCollider2D coll;
    Collider2D damageHit;
    float nextTimeToDamage = 0f;

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        if (canDamage)
        {
            sparks.Play();
        }
    }

    void Update()
    {
        damageHit = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, whatIsPlayer);

        if (canDamage)
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
    }

}
