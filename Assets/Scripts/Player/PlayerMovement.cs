using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    [SerializeField] float speed = 40f;
    [SerializeField] float minimumVelocity = 20f;

    float horizontalMove;
    float currentSpeed;

    PlayerControls controller;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerControls>();
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //if (Input.GetButtonDown("Jump"))
        //{
        //    controller.CanJump(true);
        //}
    }

    private void FixedUpdate()
    {
        PCControls();
    }

    void PCControls()
    {
        //horizontalMove = Input.GetAxisRaw("Horizontal") * currentSpeed;
        ////controller.Move(horizontalMove * Time.fixedDeltaTime);            // Player movements
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