using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float force;

    Rigidbody2D rd2d;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] float damageRadius;

    // Start is called before the first frame update
    void Awake()
    {
        rd2d = GetComponent<Rigidbody2D>();
        rd2d.gravityScale = force;
    }

    // Update is called once per frame
    void Update()
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
            Destroy(this.gameObject);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
