using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 20f;
    float projectileDamage = 20;
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

    public float GetWeaponDamage()                                              // Get the player's weapon damage
    {
        return projectileDamage;
    }

    public void WeaponDamageIncrease(float amount)                                    // Upgrade (Increase) the player's weapon damage
    {
        projectileDamage += amount;
    }

    #endregion
}