using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPotionBehaviour : MonoBehaviour
{
    [SerializeField] int recoverAmount;
    [SerializeField] LayerMask whatIsPlayer;

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x);

        if (playerCollider && Input.GetKeyDown(KeyCode.F))
        {
            playerCollider.GetComponent<PlayerStats>().IncreaseHealth(recoverAmount);
            Destroy(this.gameObject);
        }
    }
}
