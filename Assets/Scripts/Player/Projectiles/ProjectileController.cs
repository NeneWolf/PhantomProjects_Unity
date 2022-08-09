using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float damage = 20f;
    [SerializeField] float speed = 20f;
    [SerializeField] float damageRadius;

    [Header("Other Details")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsEnemy;
    //[SerializeField] GameObject weaponG;
    float projectileDamage;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }


    private void Update()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsEnemy);
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsGround);

        if (damageHit)
        {
            damageHit.gameObject.GetComponentInParent<Entity>().Damage(damage);
            Destroy(this.gameObject);
        }

        if (groundHit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}