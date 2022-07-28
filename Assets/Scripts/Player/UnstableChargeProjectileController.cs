using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableChargeProjectileController : MonoBehaviour
{
    public float speed = 10f;
    float projectileDamage;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileDamage = GameObject.Find("Player").GetComponent<PlayerAbilities>().unstableChargeDamage;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Entity>().Damage(projectileDamage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}