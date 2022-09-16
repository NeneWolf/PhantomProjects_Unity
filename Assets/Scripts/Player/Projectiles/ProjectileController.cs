using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

public class ProjectileController : MonoBehaviour
{
    GameObject upgradeManager;
    
    [SerializeField] public float damage { get; private set; }
    [SerializeField] float damageP;
    [SerializeField] float speed = 20f;
    [SerializeField] float damageRadius;

    [Header("Other Details")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsEnemy;
    float projectileDamage;
    Rigidbody2D rb;

    private void Awake()
    {
        upgradeManager = GameObject.FindObjectOfType<UpgradeManager>().gameObject;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;

        CheckDamage();

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }
    
    void CheckDamage()
    {
        if (upgradeManager.GetComponent<UpgradeManager>().gunDamage != 0)
        {
            damage = damageP + upgradeManager.GetComponent<UpgradeManager>().gunDamage;
        }
        else
        {
            damage = damageP;
        }
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