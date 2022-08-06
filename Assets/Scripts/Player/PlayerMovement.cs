using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhantomProjects.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Serializables
        [Header("Controls of the Movement")]
        [Space]
        //Get other scripts within the player
        PlayerControls playerControls;
        PlayerStats playerStats;

        //Movement fields
        float horizontalMove = 0f;
        [SerializeField] float runSpeed = 60f;                                 //Player speed

        float currentSpeed;

        bool isDead = false;

        public bool IsPlayerDead { get { return isDead; } }

        [SerializeField] GameObject bullet;
        [SerializeField] Transform bulletPoint;
        #endregion

        // Start is called before the first frame update
        void Awake()
        {
            playerControls = GetComponent<PlayerControls>();
            playerStats = GetComponent<PlayerStats>();
            currentSpeed = runSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            CheckHealth();

            if (!isDead)
            {
                PCControlsMovement();

                if (Input.GetButtonDown("Jump"))
                {
                    playerControls.CanJump(true);
                }

                ////TO REMOVE
                //if (Input.GetKeyDown(KeyCode.P))
                //{
                //    Instantiate(bullet, bulletPoint);
                //}
            }
        }

        private void FixedUpdate()
        {
        }

        void PCControlsMovement()
        {
            //Takes defaul input values and multiply by the runSpeed - Speed of the player
            horizontalMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
            playerControls.Move(horizontalMove * Time.fixedDeltaTime);
        }

        void InteractionWithObjects()
        {
            //Add the controls to interact with
            // - Doors
            // - KeyCards
        }

        void CheckHealth()
        {
            if (playerStats.ReportHealth() <= 0)
            {
                isDead = true;
            }
        }

        public void ReduceSpeed(float amount)
        {
            if(currentSpeed - amount > 25)
            {
                currentSpeed -= amount;
            }
            else if(currentSpeed - amount <= 25)
            {
                currentSpeed = 25;
            }
        }

        public void ResetSpeed()
        {
            currentSpeed = runSpeed;
        }
    }
}
