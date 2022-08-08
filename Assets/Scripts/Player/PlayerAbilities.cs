using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    //Shield Ability
    [Header("Ability 1: Shield")]
    [Space]
    [SerializeField] GameObject shield;                                     // Get shield sprite
    [SerializeField] float shieldDuration = 5.5f;                           // Set the duration for the shield (how long it will stay up)
    [SerializeField] float shieldCooldown = 20.5f;                          // Set shield cooldown (how long before the shield is available to use again)
    float shieldDurationCounter;                                            // Variable to hold timer for shield duration
    float shieldCooldownCounter;                                            // Variable to hold timer for shield cooldown
    bool shieldReady = true;                                                // Check to see if the shield is ready to be used
    public bool shieldActive { get; private set; } = false;                 // Check to see if the shield is currently active

    [Header("Ability 2: Unstable Charge Laser")]
    [Space]
    [SerializeField] public float unstableChargeDamage = 10;
    [SerializeField] public float laserTickRate = 0.7f;
    [SerializeField] public float laserCooldown = 10.5f;


    private void Start()
    {
        shieldDurationCounter = shieldDuration;                             // Set timer
        shieldCooldownCounter = shieldCooldown;                             // Set timer
    }

    private void Update()
    {
        ActivateShield();
    }

    void ActivateShield()
    {
        if (shieldReady && Input.GetKeyDown(KeyCode.E))                     // Check to see if the shield is ready to be used
        {
            shield.SetActive(true);                                         // Activate shield
            shieldReady = false;                                            // Set shield ready to false to prevent the player from constantly shielding
            shieldActive = true;                                            // Set shield active check to true

            ShieldDurationTimer();                                          // If the shield is active, start and update shield duration timer
            StartCoroutine(ShieldDuration());                               // Method to activate shield for a given amount of time
        }
        else if (!shieldReady && !shieldActive)                             // Check to see if shield is not ready and the shield isn't active
        {
            ShieldCooldownTimer();                                          // Start and update shield cooldown timer
        }
    }

    IEnumerator ShieldDuration()                                            // Method to keep shield active for the specified duration
    {
        yield return new WaitForSecondsRealtime(shieldDuration);            // How long the shield will stay up for in seconds

        shield.SetActive(false);                                            // Deactivate shield
        shieldActive = false;                                               // Set shield active check to false
        shieldCooldownCounter = shieldCooldown;                             // Set shield cooldown counter

        StartCoroutine(ShieldCooldown());                                   // Method to start shield cooldown
    }

    private void ShieldDurationTimer()                                      // Method for shield duration timer
    {
        shieldDurationCounter -= Time.deltaTime;
    }

    private void ShieldCooldownTimer()                                      // Method for shield duration timer
    {
        shieldCooldownCounter -= Time.deltaTime;
    }

    IEnumerator ShieldCooldown()                                            // Method to prepare shield for next use after cooldown period
    {
        yield return new WaitForSecondsRealtime(shieldCooldown);            // How long before the next shield activation

        shieldReady = true;                                                 // Set shield ready to be true so the player will be able to activate it again
        shieldDurationCounter = shieldDuration;                             // Set shield duration counter
    }


    public void ShieldDurationIncrease(float amount)                        // Method for upgrading (increasing) the duration of the shield (how long it will be up for).
    {
        shieldDuration += amount;
    }

    public void ShieldCooldownDecrease(float amount)                        // Method for upgrading (decreasing) the cooldown of the shield (how long before being able to use it again).
    {
        shieldCooldown -= amount;
    }

    public void UnstableChargeDamageIncrease(float amount)                  // Upgrade (Increase) the player's ability damage
    {
        unstableChargeDamage += amount;
    } 
}