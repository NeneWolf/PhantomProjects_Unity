using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : MonoBehaviour
{
    [SerializeField] private PlayerState playerStats;
    [SerializeField] private float energyRestore = 10;

    // Check to see if the player is next to the potion and is pressing the interact key
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F) && playerStats.GetCurrentEnergy() < playerStats.GetMaxEnergy())
        {
            playerStats.IncreaseEnergy(energyRestore);              // Increase the player's enery by a certain amount
            Destroy(this.gameObject);
        }
    }
}