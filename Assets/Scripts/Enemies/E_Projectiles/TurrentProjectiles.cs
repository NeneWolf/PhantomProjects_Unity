using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentProjectiles : MonoBehaviour
{
    [SerializeField]float damage;

    Rigidbody2D rd2d;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] float damageRadius;

    private void Awake()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }
 
    
    void FixedUpdate()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsGround);

        if (damageHit)
        {
            damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if (groundHit)
        {
            StartCoroutine(FlameOut());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }

    IEnumerator FlameOut()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
