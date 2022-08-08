using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PhantomProjects.Core
{
    public class PlayerControls : MonoBehaviour
    {

        #region Basic Serializables for walking & jumping

        [Header("Running & What is Ground")]
        [Space]
        [Range(0f, 0.3f)] [SerializeField] float movementSmoothing = 0.05f;     //Smooth movement
        [SerializeField] LayerMask whatIsGround;                               //Define whats ground for the player
        [SerializeField] Transform groundCheck;                                //Check transform to see if its ground

        [Header("Controls of the Movement")]
        [Space]
        const float groundRadius = 0.2f;                                       //Radius of the overlap "feet collider" to determine if its touching ground
        bool grounded;                                                         //Whether the player is touching the ground or not

        //Elements
        Rigidbody2D rb2D;                                                      //Variable to connect the player RigidBody2D

        //Player Direction
        bool facingRight = true;                                               //Player face direction for the sprite and future animation

        private Vector3 velocity = Vector3.zero;                                       //Set default velocity on the player

        [Header("Jumping")]
        [Space]
        [SerializeField] private bool airControl = false;
        [SerializeField] bool doubleJumpActive = false;                        //For the passive activation of the skill allowing the player to double jump 
        [SerializeField] int extraJumpsValue;                                  //How many jumps the player can do extra
        [SerializeField] float jumpVelocity;
        [SerializeField] float fallMultiplier = 5f;                            //Force that the player will get after jumping - Fall force application
        [SerializeField] float lowJumpMultiplier = 3f;                         //Foce exerted if the player doesnt not press long on the jump button

        int extraJumps;
        bool jump = false;

        #endregion

        private void Awake()
        {
            //Connect the player rigid body to the script
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Apply different gravity to the player giving it a better "jump" and "falling" feeling
            gravity();
        }
        private void FixedUpdate()
        {

            bool wasGrounded = grounded;
            grounded = false;

            CheckGround();

            //Check if double jump is active
            if (doubleJumpActive && grounded)
                extraJumps = extraJumpsValue;

            //if jump is true then run the Jump() function 
            if (jump)
            {
                Jump();
            }
        }

        //Checks whats ground - Ground - Surfice that the player can move on
        void CheckGround()
        {
            //Player register as "grounded" once the radius of the ground check its the layers designated as "ground"
            Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                // Checking if what is colliding is "ground"
                if (colliders[i].gameObject != gameObject)
                {
                    grounded = true;
                }

                //if the "Ground" is a "platform" with that tag, the player becomes its child
                //if not the player is removed from it
                foreach (var c in colliders)
                {
                    if (c.tag == "SpecialPlatform")
                    {
                        transform.parent = c.transform;
                    }
                    else
                    {
                        transform.parent = null;
                    }
                }
            }
        }

        //Basic Movement of the player
        public void Move(float move)
        {
            if (grounded || airControl)
            {
                // Move the character by finding the target velocity
                Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);
                // And then smoothing it out and applying it to the character
                rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmoothing);

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !facingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && facingRight)
                {
                    // ... flip the player.
                    Flip();
                }

            }
        }

        //Changes the gravity of the player
        void gravity()
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.gravityScale = fallMultiplier;
            }
            else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
                rb2D.gravityScale = lowJumpMultiplier;
            else
                rb2D.gravityScale = 1f;
        }

        //Flip the character - to be changed
        void Flip()
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        //Player Jump
        void Jump()
        {
            if (jump && extraJumps > 0)
            {
                rb2D.AddForce(new Vector2(0f, jumpVelocity));
                extraJumps--;
            }
            else if (jump && grounded && extraJumps == 0)
            {
                rb2D.AddForce(new Vector2(0f, jumpVelocity));
                jump = false;
            }

            jump = false;

        }

        public void CanJump(bool canJump)
        {
            jump = canJump;
        }
    }
}
