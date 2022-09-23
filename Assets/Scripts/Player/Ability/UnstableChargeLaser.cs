using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableChargeLaser : MonoBehaviour
{
    GameObject upgradeManager;
    
    [Header("Laser Stats")]
    [Space]
    [SerializeField] float damage = 10f;

    [SerializeField] GameObject laser;
    [SerializeField] LayerMask whatIsEnemy;

    [SerializeField] BoxCollider2D boxCollider;
    
    private void Awake()
    {
        upgradeManager = GameObject.FindObjectOfType<UpgradeManager>().gameObject;
        CheckDamage();
    }
    
    private void Update()
    {
        Collider2D damageHit = Physics2D.OverlapBox(transform.position, boxCollider.bounds.size, transform.position.x, whatIsEnemy);

        if (damageHit)
        {
            if (damageHit.gameObject.tag == "Enemy")
            {
                damageHit.gameObject.GetComponentInParent<Entity>().Damage(damage);
                StartCoroutine(wait());
            }
            else if (damageHit.gameObject.tag == "BMinion")
            {
                damageHit.gameObject.GetComponentInParent<MinionsControls>().TakeMinionDamage(damageHit.gameObject.name, damage);
                StartCoroutine(wait());
            }
        }

    }

    void CheckDamage()
    {
        if (upgradeManager.GetComponent<UpgradeManager>().abilityDamage != 0)
        {
            damage += upgradeManager.GetComponent<UpgradeManager>().abilityDamage;
        }
        else
            damage = damage;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        laser.SetActive(false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, boxCollider.bounds.size);
    }

}
