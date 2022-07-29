using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretBlock : MonoBehaviour
{
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    [SerializeField] private int numberOfTimesHit;
    [SerializeField] private LayerMask whatIsPlayerProjectile;
    [SerializeField] GameObject particle;

    Transform block;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        block = this.transform;
    }

    private void Update()
    {
        if(numberOfTimesHit <= 0)
        {
            StartCoroutine(BreakBlock());
        }
    }
    private void FixedUpdate()
    {
        Collider2D damageHit = Physics2D.OverlapBox(coll.bounds.center, coll.bounds.size, 0f, whatIsPlayerProjectile);

        if (damageHit)
        {
            Destroy(damageHit.gameObject);
            ReduceBlock();
        }

    }

    void ReduceBlock()
    {
        Instantiate(particle, block);
        numberOfTimesHit--;
    }

    IEnumerator BreakBlock()
    {
        coll.enabled = false;
        sprite.enabled = false;

        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
