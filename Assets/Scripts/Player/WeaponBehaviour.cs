using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{

    public float speed;
    //private Rigidbody2D rigidbody2;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private LayerMask whatIsDestroyable;

    //private void Start()
    //{
    //    //rigidbody2 = GetComponent<Rigidbody2D>();
    //    coll = GetComponent<BoxCollider2D>();
    //    //rigidbody2.gravityScale = 0.0f;
    //    //rigidbody2.velocity = transform.right * speed;
    //}

    //private void FixedUpdate()
    //{
    //    Collider2D damageHit = Physics2D.OverlapBox(coll.bounds.center,coll.bounds.size, 0f, whatIsEnemy);

    //    if (damageHit)
    //    {
    //        damageHit.transform.parent.GetComponent<Entity>().Damage(10f);
    //        //Destroy(this.gameObject);
    //    }

    //    print(damageHit.tag);
    //}
}
