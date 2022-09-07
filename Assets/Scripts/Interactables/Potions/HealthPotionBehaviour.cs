using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthPotionBehaviour : MonoBehaviour
{
    [SerializeField] int recoverAmount;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject player;

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x, whatIsPlayer);

        if (playerCollider && Input.GetKeyDown(KeyCode.F))
        {
            print("Healing");
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerStats>().IncreaseHealth(recoverAmount);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}
