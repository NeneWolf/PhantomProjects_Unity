using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    [SerializeField] float speed = 40f;

    float horizontalMove;
    float currentSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            controller.CanJump(true);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime);            // Player movements
    }

    public void ReduceSpeed(float amount)
    {
        if (currentSpeed - amount > 25)
        {
            currentSpeed -= amount;
        }
        else if (currentSpeed - amount <= 25)
        {
            currentSpeed = 25;
        }
    }
    
    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
