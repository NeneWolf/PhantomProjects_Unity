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
    [SerializeField] private float health;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private float weaponDamage = 20;
    [SerializeField] private float unstableChargeDamage = 35;

    public bool IsPlayerDead;

    /*
    [Header("Text Fields")]
    [Space]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI energyText;
    */

    private RectTransform uiHealth;
    private RectTransform uiEnergy;

    private Coroutine energyRegen;
    //private Coroutine healthRegen;

    #endregion

    void Start()
    {
        // Set the player's resource bars at the start
        health = maxHealth;
        //healthText.text = (int)health + " / " + maxHealth;
        //uiHealth = GameObject.Find("Health").GetComponent<RectTransform>();
        //UpdateHealthbar();

        energy = maxEnergy;
        //energyText.text = (int)energy + " / " + maxEnergy;
        //uiEnergy = GameObject.Find("Energy").GetComponent<RectTransform>();
        //UpdateEnergyBar();
    }

    void Update()
    {
        IsDead();                                                               // Method to check if the player has run out of health
        //healthText.text = (int)health + " / " + maxHealth;                      // Update player's health text
        //energyText.text = (int)energy + " / " + maxEnergy;                      // Update player's energy text
    }

    public void IsDead()
    {
        if (health <= 0)
        {
            IsPlayerDead = true;   // If the player has run out of health, reload the current level
        }
    }

    #region Health Methods

    public void UpdateHealthbar()                                              // Method to update the player's healthbar
    {
        float x = health / maxHealth;
        float y = uiHealth.localScale.y;
        float z = uiHealth.localScale.z;
        uiHealth.localScale = new Vector3(x, y, z);
    }

    public float GetCurrentHealth()                                            // Method to get the player's current health
    {
        return health;
    }

    public float GetMaxHealth()                                                // Method to get the player's max health
    {
        return maxHealth;
    }

    public void SetHealth(float h)                                             // Adjust health to not go over max health or below 0
    {
        if (h < 0) health = 0;
        else if (h > maxHealth) health = maxHealth;
        else health = h;
        //UpdateHealthbar();                                                     // Update the healthbar with these changes
    }

    public void IncreaseHealth(float h)                                        // Method for increasing the player's health
    {
        SetHealth(health + h);
    }

    public void TakeDamage(float h)                                          // Method for reducing the player's health
    {
        SetHealth(health - h);

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

            IncreaseEnergy(1);
        }
    }
    */

    #endregion

    #region Energy Methods

    public void UpdateEnergyBar()                                               // Method to update player's enerybar
    {
        float x = energy / maxEnergy;
        float y = uiEnergy.localScale.y;
        float z = uiEnergy.localScale.z;
        uiEnergy.localScale = new Vector3(x, y, z);
    }

    public float GetCurrentEnergy()                                             // Method to get the player's current energy
    {
        return energy;
    }

    public float GetMaxEnergy()                                                 // Method to get the player's current energy
    {
        return maxEnergy;
    }

    public void SetEnergy(float e)                                              // Adjust energy to not go over max energy or below 0        
    {
        if (e < 0) e = 0;
        else if (e > maxEnergy) energy = maxEnergy;
        else energy = e;
        //UpdateEnergyBar();
    }

    public void IncreaseEnergy(float e)                                         // Method to increase the player's energy
    {
        SetEnergy(energy + e);
    }

    public void ReduceEnergy(float e)                                           // Method to increase the player's energy
    {
        SetEnergy(energy - e);

        // Energy Regen over time
        if (energyRegen != null)
        {
            StopCoroutine(energyRegen);
        }

        energyRegen = StartCoroutine(EnergyRegen());
    }

    private IEnumerator EnergyRegen()
    {
        yield return new WaitForSeconds(5f);

        while (energy <= maxEnergy)
        {
            yield return new WaitForSeconds(0.5f);

            IncreaseEnergy(2);
        }
    }

    #endregion

    #region Damage Methods

    public float GetWeaponDamage()                                              // Get the player's weapon damage
    {
        return weaponDamage;
    }

    public float GetUnstableChargeDamage()                                      // Get the player's ability damage
    {
        return unstableChargeDamage;
    }

    public void WeaponDamageIncrease(float amount)                                    // Upgrade (Increase) the player's weapon damage
    {
        weaponDamage += amount;
    }

    public void UnstableChargeDamageIncrease(float amount)                                    // Upgrade (Increase) the player's ability damage
    {
        unstableChargeDamage += amount;
    }

    #endregion
}