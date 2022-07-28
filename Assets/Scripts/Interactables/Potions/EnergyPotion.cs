using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PhantomProjects.Core
{
    public class EnergyPotion : MonoBehaviour
    {
        [SerializeField] private float energyRestore = 10;

        // Check to see if the player is next to the potion and is pressing the interact key
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.tag == "Player" && Input.GetKeyDown(KeyCode.F))
            {
                collision.GetComponent<PlayerState>().IncreaseEnergy(energyRestore);            // Increase the player's enery by a certain amount         
                Destroy(this.gameObject);
            }
        }
    }
}