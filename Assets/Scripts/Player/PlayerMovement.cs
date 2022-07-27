using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    [SerializeField] float speed = 40f;
    float horizontalMove = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;                // Horizontal movement of the player

        if (Input.GetButtonDown("Jump"))                                        // Check to see if the player has pressed the jump button
        {
            controller.CanJump(true);                                           // Inform the controller that the player has pressed the jump button
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);            // Player movements
    }
}
