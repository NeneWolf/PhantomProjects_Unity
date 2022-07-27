using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    [SerializeField] float speed = 40f;
    private bool jump = false;
    float horizontalMove = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;                // Horizontal movement of the player

        if (Input.GetButtonDown("Jump"))                                        // Check to see if the player has pressed the jump button
        {
            controller.CanJump(true);                                           // Set jump to true to prevent further jumps
        }
    }

    private void FixedUpdate()
    {
        #region Player Movement

        controller.Move(horizontalMove * Time.fixedDeltaTime);            // Player movement

        #endregion
    }
}
