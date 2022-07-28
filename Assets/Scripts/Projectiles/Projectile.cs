using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private AttackDetails attackDetails;

    private float speed;
    private Rigidbody2D rigidbody2;
    private bool hasHitGround;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform damagePosition;
    [SerializeField] private float damageRadius;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        rigidbody2.gravityScale = 0.0f;
        rigidbody2.velocity = transform.right * speed;
    }

    private void Update()
    {
        if (!hasHitGround)
            attackDetails.position = transform.position;
    }
    private void FixedUpdate()
    {
        if (!hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.gameObject.GetComponent<PlayerState>().TakeDamage(attackDetails);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                hasHitGround = true;
                Destroy(gameObject);
            }
        }
    }

    public void FireProjectile(float speed, float damage)
    {
        this.speed = speed;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }
}
