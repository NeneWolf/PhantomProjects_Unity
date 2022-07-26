using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] float speed = 10f;

    float weaponDamage;
    float unstableChargeDamage;

    Rigidbody2D rb;
    PlayerState ps;
    //EnemyState es;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GameObject.Find("Player").GetComponent<PlayerState>();
        weaponDamage = ps.GetWeaponDamage();
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            //es = GameObject.Find("Enemy").GetComponent<EnemyState>();
            //es.TakeDamage(weaponDamage);
            Destroy(this.gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
