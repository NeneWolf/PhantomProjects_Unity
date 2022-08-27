using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomProjects.Managers;

public class PlayerStats : MonoBehaviour, IDataPersistance
{
    #region Variables
    PrototypeHero controls;

    [Header("Health & Energy")]
    [Space]
    [SerializeField] public float maxHealth = 100;                    // Player's maximum amount of health
    [SerializeField] public float maxEnergy = 100;                    // Player's maximum amount of energy
    public float currentHealth { get; private set; }                  // Player's current amount of health
    public float currentEnergy { get; private set; }                  // Player's current amount of energy
    public bool canUseEnergy { get; private set; }

    public bool IsPlayerDead { get; private set; }                    // Public bool to inform if the player has died or not


    [Header("Energy Regeneration - Time (Seconds)")]
    [Space]
    [SerializeField] float energyRegenAmount = 2;                   // Amount of energy restored each tick
    [SerializeField] float energyRegenRate = 1.5f;                  // The rate at which energy is restored (every 0.5 seconds will restore 2 energy)
    [SerializeField] float energyDelayPeriod = 5f;                  // Delay before energy starts to regenerate

    bool isShielded;
    public bool loadedPlayer;

    GameObject gameManager;

    #endregion

    void Start()
    {
        //gameManager = GameObject.FindObjectOfType<GameManager>().gameObject;

        //if (gameManager.GetComponent<GameManager>().playerCurrentHealth == 0)
        //{
        //    print("NO DATA");
        //    currentEnergy = maxEnergy;
        //    currentHealth = maxHealth;
        //}
        //else
        //{
        //    print("Bingo");
        //    currentEnergy = gameManager.GetComponent<GameManager>().playerCurrentEnergy;
        //    currentHealth = gameManager.GetComponent<GameManager>().playerCurrentHealth;
        //}
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;
        canUseEnergy = true;
    }

    void Update()
    {

        IsDead();                                                   // Method to check if the player has run out of health
        isShielded = GetComponentInChildren<PlayerAbilities>().shieldActive;
    }

    #region Methods

    public void IsDead()
    {
        if (currentHealth <= 0)                                     // Check's to see if the player has ran out of health
        {
            IsPlayerDead = true;
            //Destroy(gameObject);
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (currentHealth + amount >= maxHealth) currentHealth = maxHealth;         // Check to see if amount added goves over max health. If so set the player's health to max
        else currentHealth += amount;                                               // Else add the specified amount to the player's health
    }

    public void IncreaseEnergy(float amount)                                          // Method to increase the player's energy
    {
        if (currentEnergy + amount >= maxEnergy) currentEnergy = maxEnergy;         // Check to see if amount added goves over max energy. If so set the player's energy to max
        else currentEnergy += amount;                                               // Else add the specified amount to the player's energy
    }

    public void ConsumeEnergy(float amount)                           // Method to use energy
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
        if (!isShielded)                                   // When the shield is not active, the player will take damage 
        {
            if (currentHealth - attackDetails.damageAmount >= 0)    // As long as the amount of damage dealt to the player's current health is above 0, they will take damage
            {
                this.GetComponent<PrototypeHero>().Hurt();
                currentHealth -= attackDetails.damageAmount;
            }
        }
    }

    public void TakeDamage(float damageAmount)                      // Method for reducing the player's health by any other means
    {
        if (!isShielded)
        {
            if (currentHealth - damageAmount >= 0)                      // As long as the amount of damage dealt to the player's current health is above 0, they will take damage
            {
                this.GetComponent<PrototypeHero>().Hurt();
                currentHealth -= damageAmount;
            }
        }
    }

    public void SetHealthEnergyOnLoad(float health, float energy)
    {
        currentHealth = health;
        currentEnergy = energy; 
    }

    public void LoadData(GameData data)
    {
       // throw new System.NotImplementedException();
    }

    public void SaveData(GameData data)
    {
        data.currentHealth = currentHealth;
        data.currentEnergy = currentEnergy;
    }

    #endregion
}
