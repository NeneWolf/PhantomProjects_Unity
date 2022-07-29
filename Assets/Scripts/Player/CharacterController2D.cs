using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Player Movement & What is Ground / Ceiling Checks")]
	[Space]
	[Range(0, .3f)][SerializeField] private float movementSmoothing = .05f; // How much to smooth out the movement
	[SerializeField] private LayerMask whatIsGround;                            // A mask determining what is ground to the character
	[SerializeField] private Transform groundCheck;                             // A position marking where to check if the player is grounded.

	private Rigidbody2D rb;
	private Vector2 velocity = Vector2.zero;
	const float groundedRadius = .2f;                                           // Radius of the overlap circle to determine if grounded
	private bool grounded;                                                      // Whether or not the player is grounded.
	bool facingRight = true;                                                    // For determining which way the player is currently facing.

	/*
	[Header("Crouch")]
	[Space]
	[Range(0, 1)] [SerializeField] private float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[SerializeField] private Transform ceilingCheck;							// A position marking where to check for ceilings
	const float ceilingRadius = .2f;											// Radius of the overlap circle to determine if the player can stand up
	[SerializeField] private Collider2D crouchDisableCollider;					// A collider that will be disabled when crouching
	private bool wasCrouching = false;
	*/

	[Header("Jump")]
	[Space]
	[SerializeField] private bool airControl = false;                           // Whether or not a player can steer while jumping;
	[SerializeField] bool doubleJumpActive = false;                             // Whether or not the player can double jump
	[SerializeField] int extraJumpsValue;                                       // Number of extra jumps the player can peform whilst in the air
	[SerializeField] float coyoteTime = 0.2f;
	public float jumpVelocity = 6.5f;                                           // How high the player will Jump
	public float fallMultiplier = 2.5f;                                         // Gravity affecting player when they high jump
	public float lowJumpMultiplier = 2f;                                        // Gravity affecting player when they low jump
	bool jump = false;
	int extraJumps;                                                             // Stores the number of extra jumps
	float coyoteTimeCounter;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Gravity();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = grounded;
		grounded = false;

		GroundCheck();

		if (grounded)
		{
			coyoteTimeCounter = coyoteTime;
		}
		else
		{
			coyoteTimeCounter -= Time.deltaTime;
		}

		// Check to see if double jump is active
		if (doubleJumpActive && grounded)
			extraJumps = extraJumpsValue;

		if (jump)
		{
			Jump();
		}
	}

	void GroundCheck()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
			}
		}
	}

	public void Move(float move/*, bool crouch*/)
	{
		/* If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
			{
				crouch = true;
			}
		}
		*/

		//only control the player if grounded or airControl is turned on
		if (grounded || airControl)
		{
			/* If crouching
			if (crouch)
			{
				if (!wasCrouching)
				{
					wasCrouching = true;
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= crouchSpeed;

				// Disable one of the colliders when crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (crouchDisableCollider != null)
					crouchDisableCollider.enabled = true;

				if (wasCrouching)
				{
					wasCrouching = false;
				}
			}
			*/

			// Move the character by finding the target velocity
			Vector2 playerVelocity = new Vector2(move * 10f, rb.velocity.y);
			// And then smoothing it out and applying it to the character
			rb.velocity = Vector2.SmoothDamp(rb.velocity, playerVelocity, ref velocity, movementSmoothing);

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

	void Gravity()
	{
		if (rb.velocity.y < 0)
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;     // Gravity affecting the player after they jump
		}
		else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
		{
			rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;  // If jump button isn't being held carry out low jump
		}
	}

	void Jump()
	{
		if (coyoteTimeCounter > 0 && jump)
		{
			grounded = false;
			rb.velocity = Vector2.up * jumpVelocity;
			jump = false;

			coyoteTimeCounter = 0f;
		}
		else if (jump && extraJumps > 0)
		{
			rb.velocity = Vector2.up * jumpVelocity;
			extraJumps--;
		}

		jump = false;
	}

	public void CanJump(bool canJump)
	{
		jump = canJump;
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		transform.Rotate(0f, 180f, 0f);
	}

	public bool IsGrounded()
	{
		return grounded;
	}
}

