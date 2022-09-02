using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [Space]
    [SerializeField] GameObject playerCheck;
    [SerializeField] GameObject turretHead;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject bullet;

    [Header("Bullet Properties")]
    [Space]
    [SerializeField] float fireRate;
    [SerializeField] float force;
    float nextTimeToFire = 0f;

    [Header("Target")]
    [Space]
    [SerializeField] float agrooRange;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject target;
    Vector2 direction;
    GameObject BulletIns;

    void Update()
    {
        if(target == null)
            target = GameObject.FindGameObjectWithTag("Player").gameObject;

        if(target != null)
        {
            Vector2 targetPos = target.transform.position;
            direction = targetPos - (Vector2)transform.position;

            RaycastHit2D rayInfo = Physics2D.Raycast(playerCheck.transform.position, direction, agrooRange, whatIsPlayer);

            if (rayInfo)
            {
                if (targetPos.y < transform.position.y)
                {
                    turretHead.transform.up = direction;

                    if (Time.time > nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1 / fireRate;
                        ShootTarget();
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(playerCheck.transform.position, agrooRange);
    }

    void ShootTarget()
    {
        BulletIns = GameObject.Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}
