using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion3ToxicPool : MonoBehaviour
{
    [SerializeField] float reduceVelocity;
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] float cooldownToDisable;
    GameObject target;
    bool hasSlowdownPlayer = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Collider2D damageHit = Physics2D.OverlapBox(transform.position, transform.localScale, 0f,whatIsPlayer);

        if (damageHit && !hasSlowdownPlayer)
        {
            hasSlowdownPlayer = true;
            target.GetComponent<PlayerMovement>().ReduceSpeed(reduceVelocity);
            StartCoroutine(ReduceTimer());
        }
    }

    IEnumerator ReduceTimer()
    {
        yield return new WaitForSeconds(cooldownToDisable);
        target.GetComponent<PlayerMovement>().ResetSpeed();
        Destroy(this.gameObject);
    }
}
