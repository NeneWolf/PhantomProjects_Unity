using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnergyPotionBehaviour : MonoBehaviour
{
    [SerializeField] int recoverAmount;
    [SerializeField] LayerMask whatIsPlayer;

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (player != null)
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x,whatIsPlayer);

            if (playerCollider && Input.GetKeyDown(KeyCode.F))
            {
                playerCollider.GetComponent<PlayerStats>().IncreaseEnergy(recoverAmount);
                Destroy(this.gameObject);
            }
        }
    }
}
