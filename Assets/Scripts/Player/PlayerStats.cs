using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    #region Variables

    [Header("Player Stats")]
    [Space]
    [SerializeField] float currentHealth;                                      // Player's current amount of health
    [SerializeField] float maxHealth = 100;                             // Player's maximum amount of health
    [SerializeField] float currentEnergy;                 // Player's current amount of energy
    [SerializeField] public float maxEnergy { get; private set; } = 100;        // Player's maximum amount of energy
    public bool canUseEnergy { get; private set; }

    float energyRegenAmount = 2f;                                        // Amount of energy restored each tick
    float energyRegenRate = 0.5f;                                       // The rate at which energy is restored (every 0.5 seconds will restore 2 energy)
    float energyDelayPeriod = 5f;                                       // Delay before energy starts to regenerate

    public bool IsPlayerDead;                                                   // Public bool to inform if the player has died or not

    Coroutine energyRegen;                                              // Performs energy regen
    //Coroutine healthRegen;                                            // Performs health regen

    //[SerializeField] PlayerAbilities shield;

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
        IsDead();                                                               // Method to check if the player has run out of health
    }

    #region Methods

    public void IsDead()
    {
        if (currentHealth <= 0)                                                        // Check's to see if the player has ran out of health
        {
            IsPlayerDead = true;                                                // If the player runs out of health, set the player dead bool to true
            Destroy(gameObject);
        }
    }

    public void IncreaseHealth(float amount)                                         // Adjust health to not go over max health or below 0
    {
        if (currentHealth + amount >= maxHealth) currentHealth = maxHealth;                                             // If the amount being set is less than 0, set health to 0 instead
        else currentHealth += amount;                                           // Else set health to the amount
    }

    public void IncreaseEnergy(float amount)                                    // Method to increase the player's energy
    {
        if (currentEnergy + amount >= maxEnergy) currentEnergy = maxEnergy;                                             // If the amount being set is less than 0, set health to 0 instead
        else currentEnergy += amount;                                           // Use the set health method to increase the player's health by the amount
    }

    public void ConsumeEnergy(float amount)
    {
        if (currentEnergy - amount == 0)
        {
            currentEnergy = 0;
        }
        else currentEnergy -= amount;

        if (currentEnergy < maxEnergy)
        {
            StartCoroutine(EnergyRegen());
        }
    }

    public bool CanConsumeEnergy(float consume)
    {
        if (currentEnergy >= consume)
        {
            return true;
        }
        else return false;
    }

    private IEnumerator EnergyRegen()
    {
        yield return new WaitForSeconds(energyDelayPeriod);                    // Wait for the delay period to start regenerating energy

        while (currentEnergy <= maxEnergy)                                            // While energy is less than the player's maximum energy, keep regenerating energy
        {
            yield return new WaitForSeconds(energyRegenRate);                  // Wait a set amount of time each time energy is restored

            IncreaseEnergy(energyRegenAmount);                                 // Increase energy by a given amount
        }
    }

    public void TakeDamage(AttackDetails attackDetails)                                        // Method for reducing the player's health
    {
        //if (!shield.shieldActive)
        //{
        //    if (currentHealth - attackDetails.damageAmount <= 0)
        //    {
        //        currentHealth = 0;
        //        IsPlayerDead = true;
        //    }
        //    else
        //    {
        //        currentHealth -= attackDetails.damageAmount;
        //    }
        //}
    }

    #endregion
}
