using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] public float damage { get; private set; }
    [SerializeField] float damageP;
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
        damage = damageP;
    }


    private void Update()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsEnemy);
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, damageRadius, whatIsGround);

        if (damageHit)
        {
            if (damageHit.tag == "Enemy")
                damageHit.gameObject.GetComponentInParent<Entity>().Damage(damage);
            else if (damageHit.tag == "BMinion")
                damageHit.gameObject.GetComponentInParent<MinionsControls>().TakeMinionDamage(damageHit.gameObject.name, damage);

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