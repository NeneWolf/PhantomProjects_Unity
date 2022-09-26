using UnityEngine;
using System.Collections;
using System;

public class PrototypeHero : MonoBehaviour {
    
    public float      m_runSpeed = 4.5f;
    public float      m_walkSpeed = 2.0f;
    public float      m_jumpForce = 7.5f;
    public float      m_dodgeForce = 8.0f;
    public float      m_parryKnockbackForce = 4.0f; 
    public bool       m_noBlood = false;
    public bool       m_SpecialGun = false;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private SpriteRenderer      m_SR;
    private Sensor_Prototype    m_groundSensor;
    private Sensor_Prototype    m_wallSensorR1;
    private Sensor_Prototype    m_wallSensorR2;
    private Sensor_Prototype    m_wallSensorL1;
    private Sensor_Prototype    m_wallSensorL2;
    private PlayerStats         playerStats;
    private float               inputX;
    private bool                m_grounded = false;
    private bool                m_moving = false;
    private bool                m_dead;
    public  bool                m_dodging { get; private set; } = false;
    private bool                m_wallSlide = false;
    private bool                m_ledgeGrab = false;
    private bool                m_ledgeClimb = false;
    private bool                m_crouching = false;
    private Vector3             m_climbPosition;
    public  int                 m_facingDirection { get; private set; } = 1;
    private float               m_disableMovementTimer = 0.0f;
    private float               m_parryTimer = 0.0f;
    private float               m_respawnTimer = 0.0f;
    private Vector3             m_respawnPosition = Vector3.zero;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_gravity;
    public float                m_maxSpeed = 4.5f;

    //Double jump
    [SerializeField] public bool doubleJumpActive { get; private set; } = false;                             // Whether or not the player can double jump
    [SerializeField] int extraJumpsValue;                                       // Number of extra jumps the player can peform whilst in the air
    [SerializeField] float coyoteTime = 0.2f;
    bool jump = false;
    int extraJumps;                                                             // Stores the number of extra jumps
    float coyoteTimeCounter;

    [SerializeField] GameObject WeaponController;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_SR = GetComponentInChildren<SpriteRenderer>();
        playerStats = GetComponent<PlayerStats>();
        m_gravity = m_body2d.gravityScale;

        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Prototype>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_Prototype>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_Prototype>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_Prototype>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_Prototype>();

    }

    // Update is called once per frame
    void Update ()
    {
        if(Time.timeScale != 0)
        {
            //Check if the player is dead
            m_dead = playerStats.IsPlayerDead;

            // Decrease death respawn timer 
            m_respawnTimer -= Time.deltaTime;

            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            // Decrease timer that checks if we are in parry stance
            m_parryTimer -= Time.deltaTime;

            // Decrease timer that disables input movement. Used when attacking
            m_disableMovementTimer -= Time.deltaTime;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            Movement();

            if (m_grounded)
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            // Check to see if double jump is active
            if (doubleJumpActive && m_grounded)
                extraJumps = extraJumpsValue;

            Jump();

            // SlowDownSpeed helps decelerate the characters when stopping
            float SlowDownSpeed = m_moving ? 1.0f : 0.5f;

            // Set movement
            if (!m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_parryTimer < 0.0f)
            {
                if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
                    m_body2d.velocity = new Vector2(0f, m_body2d.velocity.y);
                else
                    m_body2d.velocity = new Vector2(inputX * m_maxSpeed * SlowDownSpeed, m_body2d.velocity.y);
            }

            // Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            CheckUpgradedWeapon();

            // Check if all sensors are setup properly
            if (m_wallSensorR1 && m_wallSensorR2 && m_wallSensorL1 && m_wallSensorL2)
            {
                bool prevWallSlide = m_wallSlide;
                //Wall Slide
                // True if either both right sensors are colliding and character is facing right
                // OR if both left sensors are colliding and character is facing left
                m_wallSlide = (m_wallSensorR1.State() && m_wallSensorR2.State() && m_facingDirection == 1) || (m_wallSensorL1.State() && m_wallSensorL2.State() && m_facingDirection == -1);
                if (m_grounded)
                    m_wallSlide = false;
                m_animator.SetBool("WallSlide", m_wallSlide);
                //Play wall slide sound
                if (prevWallSlide && !m_wallSlide)
                    AudioManager.instance.StopSound("WallSlide");


                //Grab Ledge
                // True if either bottom right sensor is colliding and top right sensor is not colliding 
                // OR if bottom left sensor is colliding and top left sensor is not colliding 
                bool shouldGrab = !m_ledgeClimb && !m_ledgeGrab && ((m_wallSensorR1.State() && !m_wallSensorR2.State()) || (m_wallSensorL1.State() && !m_wallSensorL2.State()));
                if (shouldGrab)
                {
                    Vector3 rayStart;
                    if (m_facingDirection == 1)
                        rayStart = m_wallSensorR2.transform.position + new Vector3(0.2f, 0.0f, 0.0f);
                    else
                        rayStart = m_wallSensorL2.transform.position - new Vector3(0.2f, 0.0f, 0.0f);

                    var hit = Physics2D.Raycast(rayStart, Vector2.down, 1.0f);

                    GrabableLedge ledge = null;
                    if (hit)
                        ledge = hit.transform.GetComponent<GrabableLedge>();

                    if (ledge)
                    {
                        m_ledgeGrab = true;
                        m_body2d.velocity = Vector2.zero;
                        m_body2d.gravityScale = 0;

                        m_climbPosition = ledge.transform.position + new Vector3(ledge.topClimbPosition.x, ledge.topClimbPosition.y, 0);
                        if (m_facingDirection == 1)
                            transform.position = ledge.transform.position + new Vector3(ledge.leftGrabPosition.x, ledge.leftGrabPosition.y, 0);
                        else
                            transform.position = ledge.transform.position + new Vector3(ledge.rightGrabPosition.x, ledge.rightGrabPosition.y, 0);
                    }
                    m_animator.SetBool("LedgeGrab", m_ledgeGrab);
                }

            }


            PlayerActions();



            //print(m_dodging);
        }
    }

    void PlayerActions()
    {
        // -- Handle Animations --
        //Death
        if (m_dead && !m_dodging)
        {
            ResetDodging();
            //m_animator.SetBool("noBlood", m_noBlood);
            m_animator.SetTrigger("Death");
            //m_respawnTimer = 2.5f;
            DisableWallSensors();
            m_body2d.mass = 1000f;
            //m_dead = true;
        }
        // Ledge Climb
        else if (Input.GetKeyDown("w") && m_ledgeGrab)
        {
            DisableWallSensors();
            m_ledgeClimb = true;
            m_body2d.gravityScale = 0;
            m_disableMovementTimer = 6.0f / 14.0f;
            m_animator.SetTrigger("LedgeClimb");
        }
        // Ledge Drop
        else if (Input.GetKeyDown("s") && m_ledgeGrab)
        {
            DisableWallSensors();
        }
        //Dodge
        else if (Input.GetKeyDown("left shift") && m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb)
        {
            m_dodging = true;
            m_animator.SetTrigger("Dodge");
            m_body2d.velocity = new Vector2(m_facingDirection * m_dodgeForce, m_body2d.velocity.y);
        }
        //Walk
        else if (m_moving && Input.GetKey(KeyCode.LeftControl) && !m_dodging)
        {
            m_animator.SetInteger("AnimState", 2);
            m_maxSpeed = m_walkSpeed;
        }
        //Run
        else if (m_moving)
        {
            m_animator.SetInteger("AnimState", 1);
            m_maxSpeed = m_runSpeed;
        }
        //Idle
        else
            m_animator.SetInteger("AnimState", 0);


    }
    
    // Function used to spawn a dust effect
    // All dust effects spawns on the floor
    // dustXoffset controls how far from the player the effects spawns.
    // Default dustXoffset is zero
    public void SpawnDustEffect(GameObject dust, float dustXOffset = 0, float dustYOffset = 0)
    {
        if (dust != null)
        {
            // Set dust spawn position
            Vector3 dustSpawnPosition = transform.position + new Vector3(dustXOffset * m_facingDirection, dustYOffset, 0.0f);
            GameObject newDust = Instantiate(dust, dustSpawnPosition, Quaternion.identity) as GameObject;
            // Turn dust in correct X direction
            newDust.transform.localScale = newDust.transform.localScale.x * new Vector3(m_facingDirection, 1, 1);
        }
    }

    void DisableWallSensors()
    {
        m_ledgeGrab = false;
        m_wallSlide = false;
        m_ledgeClimb = false;
        m_wallSensorR1.Disable(0.8f);
        m_wallSensorR2.Disable(0.8f);
        m_wallSensorL1.Disable(0.8f);
        m_wallSensorL2.Disable(0.8f);
        m_body2d.gravityScale = m_gravity;
        m_animator.SetBool("WallSlide", m_wallSlide);
        m_animator.SetBool("LedgeGrab", m_ledgeGrab);
    }

    // Called in AE_resetDodge in PrototypeHeroAnimEvents
    public void ResetDodging()
    {
        m_dodging = false;
    }

    public void SetPositionToClimbPosition()
    {
        transform.position = m_climbPosition;
        m_body2d.gravityScale = m_gravity;
        m_wallSensorR1.Disable(3.0f / 14.0f);
        m_wallSensorR2.Disable(3.0f / 14.0f);
        m_wallSensorL1.Disable(3.0f / 14.0f);
        m_wallSensorL2.Disable(3.0f / 14.0f);
        m_ledgeGrab = false;
        m_ledgeClimb = false;
    }

    public bool IsWallSliding()
    {
        return m_wallSlide;
    }

    public void DisableMovement(float time = 0.0f)
    {
        m_disableMovementTimer = time;
    }

    void RespawnHero()
    {
        transform.position = Vector3.zero;
        m_dead = false;
        m_animator.Rebind();
    }

    public void Hurt()
    {
        m_animator.SetTrigger("Hurt");
        // Disable movement 
        m_disableMovementTimer = 0.1f;
        DisableWallSensors();
    }

    void Movement()
    {
        if (!m_dead)
        {
            // -- Handle input and movement --
            inputX = 0.0f;

            if (m_disableMovementTimer < 0.0f)
                inputX = Input.GetAxis("Horizontal");

            // GetAxisRaw returns either -1, 0 or 1
            float inputRaw = Input.GetAxisRaw("Horizontal");

            // Check if character is currently moving
            if (Mathf.Abs(inputRaw) > Mathf.Epsilon && Mathf.Sign(inputRaw) == m_facingDirection)
                m_moving = true;
            else
                m_moving = false;


            // Swap direction of sprite depending on move direction
            if (inputRaw > 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb)
            {
                m_SR.flipX = false;
                m_facingDirection = 1;
            }

            else if (inputRaw < 0 && !m_dodging && !m_wallSlide && !m_ledgeGrab && !m_ledgeClimb)
            {
                m_SR.flipX = true;
                m_facingDirection = -1;
            }
        }
    }

    void Jump()
    {
        //Jump
        if (Input.GetButtonDown("Jump") && !m_dodging)
        {
            jump = true;


            if(m_wallSlide && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_disableMovementTimer < 0.0f)
            {
                m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce / 2.0f, m_jumpForce);
                m_facingDirection = -m_facingDirection;
                m_SR.flipX = !m_SR.flipX;
                jump = false;

                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_groundSensor.Disable(0.2f);
            }
            else if(m_grounded && !m_dodging && !m_ledgeGrab && !m_ledgeClimb && !m_crouching && m_disableMovementTimer < 0.0f)
            {
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);

                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_groundSensor.Disable(0.2f);
            }
            else if (jump && extraJumps > 0)
            {
                m_body2d.velocity = new Vector2(-m_facingDirection * m_jumpForce / 2.0f, m_jumpForce);
                m_facingDirection = -m_facingDirection;
                m_SR.flipX = !m_SR.flipX;
                extraJumps--;

                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_groundSensor.Disable(0.2f);
            }


        }

        jump = false;

    }

    void CheckUpgradedWeapon()
    {
        // Change to Upgrade Weapon Animation
        if (WeaponController != null)
        {
            if (WeaponController.GetComponent<PlayerWeapon>().hasUpgradedWeapon)
                m_SpecialGun = true;
        }

        // Set Animation layer for special gun
        int boolInt = m_SpecialGun ? 1 : 0;
        m_animator.SetLayerWeight(1, boolInt);
        //m_animator.SetLayerWeight(0, 0);
    }

    public void ReduceSpeed(float amount)
    {
        var minimumVelocity = m_runSpeed / 2;
        
        if (m_runSpeed - amount > minimumVelocity)
        {
            m_runSpeed -= amount;
        }
        else if (m_runSpeed - amount <= minimumVelocity)
        {
            m_runSpeed = minimumVelocity;
        }
    }
    
    public void ResetSpeed()
    {
        m_runSpeed = 6f;
    }

    public void DoubleJump(bool jumpD)
    {
        doubleJumpActive = jumpD;
    }
}
