using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private float healthRestore = 10;

    // Check to see if the player is next to the potion and is pressing the interact key
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            collision.GetComponent<PlayerState>().IncreaseHealth(healthRestore);            // Restore the player's health by a certain amount      
            Destroy(this.gameObject);
        }
    }
}
