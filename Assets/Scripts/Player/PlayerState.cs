using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerState : MonoBehaviour
{
    #region Variables

    [Header("Player Stats")]
    [Space]
    [SerializeField] private float health;                                      // Player's current amount of health
    [SerializeField] private float maxHealth = 100;                             // Player's maximum amount of health
    [SerializeField] private float energy;                                      // Player's current amount of energy
    [SerializeField] private float maxEnergy = 100;                             // Player's maximum amount of energy

    private float energyRegenAmount = 2;                                        // Amount of energy restored each tick
    private float energyRegenRate = 0.5f;                                       // The rate at which energy is restored (every 0.5 seconds will restore 2 energy)
    private float energyDelayPeriod = 5f;                                       // Delay before energy starts to regenerate

    public bool IsPlayerDead;                                                   // Public bool to inform if the player has died or not

    private Coroutine energyRegen;                                              // Performs energy regen
    //private Coroutine healthRegen;                                            // Performs health regen

    #endregion

    void Start()
    {
        // Set the player's health and energy to their maximum amount on start
        health = maxHealth;
        energy = maxEnergy;
    }

    void Update()
    {
        IsDead();                                                               // Method to check if the player has run out of health
    }

    public void IsDead()
    {
        if (health <= 0)                                                        // Check's to see if the player has ran out of health
        {
            IsPlayerDead = true;                                                // If the player runs out of health, set the player dead bool to true
        }
    }

    #region Health Methods

    public void SetHealth(float amount)                                         // Adjust health to not go over max health or below 0
    {
        if (amount < 0) health = 0;                                             // If the amount being set is less than 0, set health to 0 instead
        else if (amount > maxHealth) health = maxHealth;                        // If the amount being set is greater than the player's maximum health, set the health to the maximum health instead
        else health = amount;                                                   // Else set health to the amount
    }

    public void IncreaseHealth(float amount)                                    // Method for increasing the player's health
    {
        SetHealth(health + amount);                                             // Use the set health method to increase the player's health by the amount
    }

    public void TakeDamage(float damage)                                        // Method for reducing the player's health
    {
        SetHealth(health - damage);                                             // Use the set health method to decrease the player's health by damage

        /*
        // Health regen over time if needed
        if (healthRegen != null)
        {
            StopCoroutine(healthRegen);
        }

        healthRegen = StartCoroutine(HealthRegen());
        */
    }

    /*private IEnumerator HealthRegen()
    {
        yield return new WaitForSeconds(5f);

        while (health <= maxHealth)
        {
            yield return new WaitForSeconds(3f);

            IncreaseHealth(1);
        }
    }
    */

    #endregion

    #region Energy Methods

    public float GetCurrentEnergy()                                             // Method to get the player's current energy
    {
        return energy;
    }

    public float GetMaxEnergy()                                                 // Method to get the player's current energy
    {
        return maxEnergy;
    }

    public void SetEnergy(float amount)                                         // Adjust energy to not go over max energy or below 0        
    {
        if (amount < 0) energy = 0;                                             // If the amount being set is less than 0, set energy to 0 instead
        else if (amount > maxEnergy) energy = maxEnergy;                        // If the amount being set is greater than the player's maximum energy, set the energy to the maximum energy instead
        else energy = amount;                                                   // Else set energy to the amount
    }

    public void IncreaseEnergy(float amount)                                    // Method to increase the player's energy
    {
        SetEnergy(energy + amount);                                             // Use the set health method to increase the player's health by the amount
    }

    public void ReduceEnergy(float amount)                                      // Method to increase the player's energy
    {
        SetEnergy(energy - amount);                                             // Use the set health method to decrease the player's energy by the amount

        // Energy Regen over time
        if (energyRegen != null)
        {
            StopCoroutine(energyRegen);
        }

        energyRegen = StartCoroutine(EnergyRegen());                            // Start regenerating energy
    }

    private IEnumerator EnergyRegen()
    {
        yield return new WaitForSeconds(energyDelayPeriod);                    // Wait for the delay period to start regenerating energy

        while (energy <= maxEnergy)                                            // While energy is less than the player's maximum energy, keep regenerating energy
        {
            yield return new WaitForSeconds(energyRegenRate);                  // Wait a set amount of time each time energy is restored

            IncreaseEnergy(energyRegenAmount);                                 // Increase energy by a given amount
        }
    }

    #endregion
}