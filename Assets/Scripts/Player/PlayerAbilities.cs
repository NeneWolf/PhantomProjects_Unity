using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public CharacterController2D controller;
    public PlayerState ps;

    //Shield Ability
    [Header("Ability 1: Shield")]
    [Space]
    [SerializeField] private GameObject shield;                                 // Get shield sprite
    [SerializeField] private GameObject shieldDurationUITimer;                  // UI Timer for duration
    [SerializeField] private GameObject shieldCooldownUITimer;                  // UI Timer for cooldown

    private TextMeshProUGUI shieldDurationText;                                 // Get the text for the duration timer to be updated
    private TextMeshProUGUI shieldCooldownText;                                 // Get the text for the cooldown timer to be updated

    private float shieldDuration = 5.5f;                                        // Set the duration for the shield (how long it will stay up)
    private float shieldCooldown = 20.5f;                                       // Set shield cooldown (how long before the shield is available to use again)
    private float shieldDurationCounter;                                        // Timer for shield duration
    private float shieldCooldownCounter;                                        // Timer for shield cooldown
    private bool shieldReady = true;                                            // Check to see if the shield is ready to be used
    private bool shieldActive = false;                                          // Check to see if the shield is currently active

    [Header("Ability 2: Unstable Charge")]
    [Space]
    [SerializeField] GameObject energyProjectilePrefab;                         // Get the enery projectile sprite
    [SerializeField] float attackInterval = 0.5f;                               // Time before each attack
    int chargeEnergyCost;                                                       // Cost for using this ability
    float attackTimer = 0;

    private void Start()
    {
        shieldDurationText = shieldDurationUITimer.GetComponentInChildren<TextMeshProUGUI>();       // Get the text for the duration timer to be updated
        shieldCooldownText = shieldCooldownUITimer.GetComponentInChildren<TextMeshProUGUI>();       // Get the text for the cooldown timer to be updated

        shieldDurationCounter = shieldDuration;                                 // Set timer
        shieldCooldownCounter = shieldCooldown;                                 // Set timer
    }

    private void Update()
    {
        if (shieldReady)                                                        // Check to see if the shield is ready to be used
        {
            if (Input.GetKeyDown(KeyCode.E))                                    // Assign shield activation hotkey and check if has been pressed
            {
                shield.SetActive(true);                                         // Activate shield
                shieldReady = false;                                            // Set shield ready to false to prevent the player from constantly shielding
                shieldActive = true;                                            // Set shield active check to true

                StartCoroutine(ShieldDuration());                               // Method to activate shield for a given amount of time
            }
        }
        else if (!shieldReady && !shieldActive)                                 // Check to see if shield is not ready and the shield isn't active
        {
            ShieldCooldownTimer();                                              // Start and update shield cooldown timer
        }

        if (shieldActive)
        {
            ShieldDurationTimer();                                              // If the shield is active, start and update shield duration timer
        }

        if (attackTimer <= 0)
        {
            attackTimer = 0;                                                    // Set attack timer to 0 so it doesnt go into negatives
            UnstableCharge();                                                   // Method to peform the player's Unstable Charge ability
        }
        else
        {
            attackTimer -= Time.deltaTime;                                      // Count down timer every second
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")                                      // Check to see if a projectile has collided with the shield
        {
            Destroy(collision.gameObject);                                      // Destroy that projectile
            //collision.GetComponent<EnemyState>().NullifyDamage();               // Nullify the damage taken from that projectile
        }
    }

    IEnumerator ShieldDuration()                                                // Method to keep shield active for the specified duration
    {
        shieldDurationUITimer.SetActive(true);                                  // Display duration timer on the player's UI

        yield return new WaitForSecondsRealtime(shieldDuration);                // How long the shield will stay up for in seconds

        shieldDurationUITimer.SetActive(false);                                 // Hide shield duration timer from UI
        shield.SetActive(false);                                                // Deactivate shield
        shieldActive = false;                                                   // Set shield active check to false
        shieldCooldownCounter = shieldCooldown;                                 // Set shield cooldown counter

        StartCoroutine(ShieldCooldown());                                       // Method to start shield cooldown
    }

    private void ShieldDurationTimer()                                          // Method for shield duration timer
    {
        shieldDurationCounter -= Time.deltaTime;
        shieldDurationText.text = "Shield Duration: " + (int)shieldDurationCounter;

        if (shieldDurationCounter < 0)
        {
            shieldDurationCounter = 0;
        }
    }

    private void ShieldCooldownTimer()                                          // Method for shield duration timer
    {
        shieldCooldownCounter -= Time.deltaTime;
        shieldCooldownText.text = "Shield Cooldown: " + (int)shieldCooldownCounter;

        if (shieldCooldownCounter < 0)
        {
            shieldCooldownCounter = 0;
        }
    }

    IEnumerator ShieldCooldown()                                                // Method to prepare shield for next use after cooldown period
    {
        shieldCooldownUITimer.SetActive(true);                                  // Display cooldown timer on player's UI

        yield return new WaitForSecondsRealtime(shieldCooldown);                // How long before the next shield activation

        shieldCooldownUITimer.SetActive(false);                                 // Hide shield cooldown from UI
        shieldReady = true;                                                     // Set shield ready to be true so the player will be able to activate it again
        shieldDurationCounter = shieldDuration;                                 // Set shield duration counter
    }

    public void ShieldDurationIncrease(float amount)                            // Method for upgrading (increasing) the duration of the shield (how long it will be up for).
    {
        shieldDuration += amount;
    }

    public void ShieldCooldownDecrease(float amount)                            // Method for upgrading (decreasing) the cooldown of the shield (how long before being able to use it again).
    {
        shieldCooldown -= amount;
    }

    private void UnstableCharge()                                               // Method for the Unstable Charge ability
    {
        chargeEnergyCost = (int)ps.GetMaxEnergy() / 2;

        if (Input.GetMouseButtonDown(1) && ps.GetCurrentEnergy() >= chargeEnergyCost)           // Check to see if the player has enough energy to use the ability and the appropriate mouse button has been pressed
        {
            if (!controller.facingRight)                                        // Check to see which way the player is facing
            {
                SpawnProjectile(-0.5f, Vector2.left);                           // If facing to the left, spawn and launch projectile to the left side (-0.5f)
                ps.ReduceEnergy(chargeEnergyCost);                              // Reduce the player's energy by the specified amount
            }
            else
            {
                SpawnProjectile(0.5f, Vector2.right);                           // Else if the player is facing right, spawn and launch projectile to the left side (0.5f)
                ps.ReduceEnergy(chargeEnergyCost);                              // Reduce the player's energy by the specified amount
            }

            attackTimer = attackInterval;                                       // Reset attack timer so the player has to wait before using the attack again
        }
    }

    private void SpawnProjectile(float num, Vector2 direction)                  // Method for spawning projectiles
    {
        Vector2 spawnEnergyProjectile = new Vector2(transform.position.x + num, transform.position.y);
        GameObject instance = Instantiate(energyProjectilePrefab, spawnEnergyProjectile, Quaternion.identity);
        instance.GetComponent<UnstableChargeProjectileController>().SetVelocity(direction);
    }
}