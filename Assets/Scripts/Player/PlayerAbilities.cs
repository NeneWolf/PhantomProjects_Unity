using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] PlayerState ps;

    //Shield Ability
    [Header("Ability 1: Shield")]
    [Space]
    [SerializeField] GameObject shield;                                 // Get shield sprite
    [SerializeField] float shieldDuration = 5.5f;                       // Set the duration for the shield (how long it will stay up)
    [SerializeField] float shieldCooldown = 20.5f;                      // Set shield cooldown (how long before the shield is available to use again)
    float shieldDurationCounter;                                        // Variable to hold timer for shield duration
    float shieldCooldownCounter;                                        // Variable to hold timer for shield cooldown
    bool shieldReady = true;                                            // Check to see if the shield is ready to be used
    public bool shieldActive { get; private set; } = false;                                          // Check to see if the shield is currently active

    [Header("Ability 2: Unstable Charge")]
    [Space]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject energyProjectilePrefab;                         // Get the enery projectile sprite
    [SerializeField] public float unstableChargeDamage { get; private set; } = 35;
    [SerializeField] float attackInterval = 0.5f;                               // Time before each attack
    int chargeEnergyCost;                                                       // Cost for using this ability
    float attackTimer = 0;

    private void Start()
    {
        shieldDurationCounter = shieldDuration;                                 // Set timer
        shieldCooldownCounter = shieldCooldown;                                 // Set timer
    }

    private void Update()
    {
        ActivateShield();
        AttackTimer();
    }

    #region Ability 1: Unstable Charge

    void AttackTimer()
    {
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

    private void UnstableCharge()                                               // Method for the Unstable Charge ability
    {
        chargeEnergyCost = (int)ps.maxEnergy / 2;

        if (Input.GetMouseButtonDown(1) && ps.CanConsumeEnergy(chargeEnergyCost))           // Check to see if the player has enough energy to use the ability and the appropriate mouse button has been pressed
        {
            Shoot();
            ps.ConsumeEnergy(chargeEnergyCost);
            attackTimer = attackInterval;                                       // Reset attack timer so the player has to wait before using the attack again
        }
    }

    void Shoot()
    {
        Instantiate(energyProjectilePrefab, firePoint.position, firePoint.rotation);
    }

    #endregion

    #region Ability 2: Shield

    void ActivateShield()
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
    }

    IEnumerator ShieldDuration()                                                // Method to keep shield active for the specified duration
    {
        yield return new WaitForSecondsRealtime(shieldDuration);                // How long the shield will stay up for in seconds

        shield.SetActive(false);                                                // Deactivate shield
        shieldActive = false;                                                   // Set shield active check to false
        shieldCooldownCounter = shieldCooldown;                                 // Set shield cooldown counter

        StartCoroutine(ShieldCooldown());                                       // Method to start shield cooldown
    }

    private void ShieldDurationTimer()                                          // Method for shield duration timer
    {
        shieldDurationCounter -= Time.deltaTime;
        FixValues(shieldDurationCounter);
    }

    private void ShieldCooldownTimer()                                          // Method for shield duration timer
    {
        shieldCooldownCounter -= Time.deltaTime;
        FixValues(shieldCooldownCounter);
    }

    private void FixValues(float timer)
    {
        if (timer < 0)
        {
            timer = 0;
        }
    }

    IEnumerator ShieldCooldown()                                                // Method to prepare shield for next use after cooldown period
    {
        yield return new WaitForSecondsRealtime(shieldCooldown);                // How long before the next shield activation

        shieldReady = true;                                                     // Set shield ready to be true so the player will be able to activate it again
        shieldDurationCounter = shieldDuration;                                 // Set shield duration counter
    }

    #endregion

    #region Shield Upgrade Methods

    public void ShieldDurationIncrease(float amount)                            // Method for upgrading (increasing) the duration of the shield (how long it will be up for).
    {
        shieldDuration += amount;
    }

    public void ShieldCooldownDecrease(float amount)                            // Method for upgrading (decreasing) the cooldown of the shield (how long before being able to use it again).
    {
        shieldCooldown -= amount;
    }

    #endregion


    #region Unstable Charge Upgrade Method

    public void UnstableChargeDamageIncrease(float amount)                                    // Upgrade (Increase) the player's ability damage
    {
        unstableChargeDamage += amount;
    }

    #endregion
}