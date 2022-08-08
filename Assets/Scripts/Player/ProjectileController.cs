using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] LayerMask whatIsPlayerProjectile;

    public float speed = 20f;
    float projectileDamage;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileDamage = GameObject.Find("Weapon").GetComponent<PlayerWeapon>().pistolDamage;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.transform.parent.GetComponent<Entity>().Damage(projectileDamage);
            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}