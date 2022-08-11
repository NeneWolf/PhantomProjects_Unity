using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    AttackDetails attackDetails;

    float speed;
    Rigidbody2D rigidbody2;
    bool hasHitGround;

    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] Transform damagePosition;
    [SerializeField] float damageRadius;
    [SerializeField] bool hasAnimation = false;

    Animator animation;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
        animation = GetComponent<Animator>();
        rigidbody2.gravityScale = 0.0f;
        rigidbody2.velocity = transform.right * speed;

        if (hasAnimation)
            StartCoroutine(TimeToDestroy());
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
                damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(attackDetails);
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

    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        animation.SetBool("explode", true);

    }
}
