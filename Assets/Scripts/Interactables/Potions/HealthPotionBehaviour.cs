using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HealthPotionBehaviour : MonoBehaviour
    {

        [SerializeField] int recoverAmount;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
               collision.gameObject.GetComponent<PlayerStats>().IncreaseHealth(recoverAmount);

                Destroy(this.gameObject);
            }
        }
    }
