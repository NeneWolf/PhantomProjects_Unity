using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healthRestore = 10;

    // Check to see if the player is next to the potion and is pressing the interact key
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            collision.GetComponent<PlayerState>().IncreaseHealth(healthRestore);            // Restore the player's health by a specified amount
            Destroy(this.gameObject);                                                       // Remove the potion from the level
        }
    }
}
