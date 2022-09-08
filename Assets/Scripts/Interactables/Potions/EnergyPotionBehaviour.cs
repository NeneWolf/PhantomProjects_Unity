using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnergyPotionBehaviour : MonoBehaviour
{
    [SerializeField] int recoverAmount;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject player;

    private void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x, whatIsPlayer);

        if (playerCollider && Input.GetKeyDown(KeyCode.F))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerStats>().IncreaseEnergy(recoverAmount);
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}