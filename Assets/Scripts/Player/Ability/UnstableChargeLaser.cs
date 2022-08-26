using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableChargeLaser : MonoBehaviour
{
    [Header("Laser Stats")]
    [Space]
    [SerializeField] float damage = 10f;

    Collider2D damageHit;

    [SerializeField] GameObject laser;
    [SerializeField] LayerMask whatIsEnemy;

    private void Update()
    {
        damageHit = Physics2D.OverlapBox(GetComponent<CapsuleCollider2D>().transform.position, new Vector2(5f, 0.5f), whatIsEnemy);

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

    IEnumerator wait()
    {
        
        yield return new WaitForSeconds(0.02f);
        laser.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(GetComponent<CapsuleCollider2D>().transform.position, new Vector2(5f, 0.5f));
    }

}
