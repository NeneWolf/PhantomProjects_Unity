using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;

    [SerializeField] float speed = 40f;
    [SerializeField] float minimumVelocity = 20f;

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
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            controller.CanJump(true);
        }
    }

    private void FixedUpdate()
    {
        PCControls();
    }

    void PCControls()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
        controller.Move(horizontalMove * Time.fixedDeltaTime);            // Player movements
    }

    public void ReduceSpeed(float amount)
    {
        if (currentSpeed - amount > minimumVelocity)
        {
            currentSpeed -= amount;
        }
        else if (currentSpeed - amount <= minimumVelocity)
        {
            currentSpeed = minimumVelocity;
        }
    }
    
    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
}
