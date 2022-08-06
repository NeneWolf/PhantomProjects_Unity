using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    [Header("Properties")]
    [Space]
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

    Transform target;
    Vector2 direction;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPos = target.position;
        direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, agrooRange, whatIsPlayer);

        if (rayInfo)
        {
            if(targetPos.y < transform.position.y)
            {
                //playerDetected = true;
                turretHead.transform.up = direction;

                if (Time.time > nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1 / fireRate;
                    ShootTarget();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, agrooRange);
    }

    void ShootTarget()
    {
        GameObject BulletIns = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}
