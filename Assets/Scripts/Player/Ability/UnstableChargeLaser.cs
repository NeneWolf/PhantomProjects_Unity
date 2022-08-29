using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableChargeLaser : MonoBehaviour
{
    GameObject upgradeManager;
    
    [Header("Laser Stats")]
    [Space]
    [SerializeField] float damage = 10f;

    Collider2D damageHit;

    [SerializeField] GameObject laser;
    [SerializeField] LayerMask whatIsEnemy;

    private void Awake()
    {
        upgradeManager = GameObject.FindObjectOfType<UpgradeManager>().gameObject;
        CheckDamage();
    }
    
    private void Update()
    {
        damageHit = Physics2D.OverlapBox(this.transform.position, new Vector2(5f, 0.5f), whatIsEnemy);

        if (damageHit)
        {
            if (damageHit.tag == "Enemy")
            {
                damageHit.gameObject.GetComponentInParent<Entity>().Damage(damage);
                StartCoroutine(wait());
            }
            else if (damageHit.tag == "BMinion")
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
        
        yield return new WaitForSeconds(0.02f);
        laser.SetActive(false);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(this.transform.position, new Vector2(5f, 0.5f));
    }

}
