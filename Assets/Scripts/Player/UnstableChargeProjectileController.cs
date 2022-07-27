using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableChargeProjectileController : MonoBehaviour
{
    public float speed = 10f;
    float projectileDamage = 35;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    #region Get Damage + Upgrade Method

    public float GetUnstableChargeDamage()                                      // Get the player's ability damage
    {
        return projectileDamage;
    }

    public void UnstableChargeDamageIncrease(float amount)                                    // Upgrade (Increase) the player's ability damage
    {
        projectileDamage += amount;
    }

    #endregion
}