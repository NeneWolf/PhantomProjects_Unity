using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion3ToxicPool : MonoBehaviour
{
    [SerializeField] float reduceVelocity;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float cooldownToDisable;
    GameObject target;
    bool hasSlowdownPlayer = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        
        Collider2D damageHit = Physics2D.OverlapBox(transform.position, transform.localScale, 0f,whatIsPlayer);
        Collider2D groundHit = Physics2D.OverlapBox(transform.position, transform.localScale, 0f, whatIsGround);
        
        if (damageHit && !hasSlowdownPlayer)
        {
            hasSlowdownPlayer = true;
            target.gameObject.GetComponent<PrototypeHero>().ReduceSpeed(reduceVelocity);
            StartCoroutine(ReduceTimer());
        }
    }

    IEnumerator ReduceTimer()
    {
        yield return new WaitForSeconds(cooldownToDisable);
        target.gameObject.GetComponent<PrototypeHero>().ResetSpeed();
        Destroy(this.gameObject);
    }
}
