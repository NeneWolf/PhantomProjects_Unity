using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    #region Variables

    [Header("Health & Energy")]
    [Space]
    [SerializeField] public int maxHealth = 100;                    // Player's maximum amount of health
    [SerializeField] public int maxEnergy = 100;                    // Player's maximum amount of energy
    public int currentHealth { get; private set; }                  // Player's current amount of health
    public int currentEnergy { get; private set; }                  // Player's current amount of energy
    public bool canUseEnergy { get; private set; }

    [Header("Energy Regeneration - Time (Seconds)")]
    [Space]
    [SerializeField] int energyRegenAmount = 2;                     // Amount of energy restored each tick
    [SerializeField] float energyRegenRate = 1.5f;                  // The rate at which energy is restored (every 0.5 seconds will restore 2 energy)
    [SerializeField] float energyDelayPeriod = 5f;                  // Delay before energy starts to regenerate

    public bool IsPlayerDead;                                       // Public bool to inform if the player has died or not

    [SerializeField] PlayerAbilities shield;

    #endregion

    void Start()
    {
        // Set the player's health and energy to their maximum amount on start
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        canUseEnergy = true;
    }

    void Update()
    {
        IsDead();                                                   // Method to check if the player has run out of health
    }

    #region Methods

    public void IsDead()
    {
        if (currentHealth <= 0)                                     // Check's to see if the player has ran out of health
        {
            IsPlayerDead = true;                                    // If the player runs out of health, set the player dead bool to true
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth(int amount)
    {
        if (currentHealth + amount >= maxHealth) currentHealth = maxHealth;         // Check to see if amount added goves over max health. If so set the player's health to max
        else currentHealth += amount;                                               // Else add the specified amount to the player's health
    }

    public void IncreaseEnergy(int amount)                                          // Method to increase the player's energy
    {
        if (currentEnergy + amount >= maxEnergy) currentEnergy = maxEnergy;         // Check to see if amount added goves over max energy. If so set the player's energy to max
        else currentEnergy += amount;                                               // Else add the specified amount to the player's energy
    }

    public void ConsumeEnergy(int amount)                           // Method to use energy
    {
        if (currentEnergy - amount == 0)                            // If the player has no energy and they attemp to consume energy, keep their current energy at 0 instead of going into negatives.
        {
            currentEnergy = 0;
        }
        else currentEnergy -= amount;                               // Reduce the player's current amount of energy by the amount specified

        if (currentEnergy < maxEnergy)
        {
            StartCoroutine(EnergyRegen());                          // If energy has been consumed start regenerating energy
        }
    }

    public bool CanConsumeEnergy(float consume)                     // Check to see if the player has energy to consume
    {
        if (currentEnergy >= consume)
        {
            return true;                                            // If the player has energy, they will be allowed to use it
        }
        else return false;
    }

    private IEnumerator EnergyRegen()
    {
        yield return new WaitForSeconds(energyDelayPeriod);         // Wait for the delay period to start regenerating energy

        while (currentEnergy <= maxEnergy)                          // While energy is less than the player's maximum energy, keep regenerating energy
        {
            yield return new WaitForSeconds(energyRegenRate);       // Wait a set amount of time each time energy is restored

            IncreaseEnergy(energyRegenAmount);                      // Increase energy by a given amount
        }
    }

    public void TakeDamage(AttackDetails attackDetails)             // Method for reducing the player's health by enemies
    {
        if (!shield.shieldActive)                                   // When the shield is not active, the player will take damage 
        {
            if (currentHealth - attackDetails.damageAmount >= 0)    // As long as the amount of damage dealt to the player's current health is above 0, they will take damage
            {
                currentHealth -= (int)attackDetails.damageAmount;
            }
        }
    }

    public void TakeDamage(float damageAmount)                      // Method for reducing the player's health by any other means
    {
        if (currentHealth - damageAmount >= 0)                      // As long as the amount of damage dealt to the player's current health is above 0, they will take damage
        {
            currentHealth -= (int)damageAmount;
        }
    }

    #endregion
}