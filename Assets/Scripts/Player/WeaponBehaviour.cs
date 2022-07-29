using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    private AttackDetails attackDetails;
    public float damage;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask whatIsEnemy;
    //[SerializeField] private Transform damagePosition;
    //[SerializeField] private float damageRadius;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Collider2D detectedObjects = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, whatIsEnemy);
        
        ////foreach(Collider2D collider in detectedObjects)
        ////{
        ////    collider.transform.parent.GetComponent<Entity>().Damage(damage);
        ////}

        //if(detectedObjects.tag == "Enemy")
        //{
        //    detectedObjects.transform.parent.GetComponent<Entity>().Damage(damage);
        //    print("Hit");
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            collision.transform.parent.GetComponent<Entity>().Damage(damage);
            print(collision.transform.parent);
        }
    }



}
