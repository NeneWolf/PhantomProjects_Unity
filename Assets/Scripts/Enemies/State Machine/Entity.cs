using PhantomProjects.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public string name;

    //Rewards
    [Header ("Rewards")]
    CanvasUI ui;
    public int mutationPoints;
    protected bool canGiveReward = true;


    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public int facingDirection { get; private set; }
    public Rigidbody2D rb2d { get; private set; }
    public CapsuleCollider2D bc2d { get; private set; }
    public Animator animator { get; private set; }
    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }

    [Header("Checks")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private Transform groundCheck;

    [Header("Spawn Minions")]
    [Space]
    [SerializeField] private bool canSpawnMinions = false;
    [SerializeField] private bool spawnAfterDead = false;
    [SerializeField] private int spawnCount;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject enemySpawn;

    public bool hasBeenDamage { get; private set; }

    private bool hasSpawnMinions;

    public float currentHealth { get; private set; }
    protected bool dropPondDead = false;
    protected bool isDead;
    
    Vector2 velocityWorkspace;

    public virtual void Start()
    {
        facingDirection = 1;
        aliveGO = transform.Find("Alive").gameObject;
        rb2d = aliveGO.GetComponent<Rigidbody2D>();
        bc2d = aliveGO.GetComponent<CapsuleCollider2D>();
        animator = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        ui = GameObject.FindObjectOfType<CanvasUI>();
        currentHealth = entityData.maxHealth;
        hasBeenDamage = false;

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();

        animator.SetFloat("yVelocity", rb2d.velocity.y);

        if(canSpawnMinions && hasSpawnMinions == false)
        {
            if (spawnAfterDead  && currentHealth == 0)
            {
                SpawnMinions();
                hasSpawnMinions = true;
            }
            else if (!spawnAfterDead && currentHealth <= (entityData.maxHealth / 2))
            {
                SpawnMinions();
                hasSpawnMinions = true;
            }
        }

        if (isDead && dropPondDead)
        {
            rb2d.gravityScale = 0.5f;
            bc2d.enabled = false;
        }
        else if(isDead && !dropPondDead)
        {
            rb2d.gravityScale = 0f;
            bc2d.enabled = false;
        }


    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb2d.velocity.y);
        rb2d.velocity = velocityWorkspace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();

        velocityWorkspace.Set(angle.x * velocity *direction, angle.y * velocity);
        rb2d.velocity = velocityWorkspace;
    }

    //Check for Wall and Ledge
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    //Check for the player
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInRangePosition()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(rb2d.velocity.x, velocity);
        rb2d.velocity = velocityWorkspace;
    }

    public virtual void Damage(float dmg) 
    {
        print("Enemy HP: " + currentHealth);
        hasBeenDamage = true;

        currentHealth -= dmg;

        if(!isDead)
            Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f)));

        if (currentHealth <= 0)
        {
            isDead = true;

            if (canGiveReward)
            {
                GiveRewards(mutationPoints);
                canGiveReward = false;
            }
        }
    }

    public virtual void Heal(float amount)
    {
        if (!isDead && currentHealth != entityData.maxHealth)
        {
            if (currentHealth + amount < entityData.maxHealth)
            {
                currentHealth += amount;
            }
            else if (currentHealth + amount >= entityData.maxHealth)
            {
                currentHealth = entityData.maxHealth;
            }
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
    }

    public virtual void SpawnMinions()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            //TODO: Change this...
            Instantiate(enemySpawn, new Vector3(spawnPosition.transform.position.x + Random.Range(-10.0f,10.0f), spawnPosition.transform.position.y, 0f), Quaternion.identity);
        }
    }

    public virtual void GiveRewards(int points)
    {
        ui.MutationPointsCollection(points);
    }
}
