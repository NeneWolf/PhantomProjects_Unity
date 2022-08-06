using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2MinionToxicBall : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject toxicpool;
    bool hasSpawnPool = false;

    GameObject target;
    float speed = 10f;
    float diff;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        diff = target.transform.position.x - transform.position.x;
        
    }

    void Update()
    {
        transform.Translate(transform.right * diff * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Collider2D damageHit = Physics2D.OverlapCircle(transform.position,0.5f, whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapCircle(transform.position, 0.5f, whatIsGround);

        if (damageHit)
        {
            damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }

        if (groundHit && !hasSpawnPool)
        {
            hasSpawnPool = true;
            GameObject.Instantiate(toxicpool, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z),toxicpool.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }

}
