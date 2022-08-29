using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Character information
    public bool loadedPlayer;
    public int characterSelected;

    //Player Stats
    public float currentHealth;
    public float currentEnergy;
    public int mutationPoints;

    //Level
    public int currentLevelIndex;
    public int modeSelected;

    //Skill Tree
    public bool saveTree;
    public string skillTree;

    // Upgrades
    public float shieldDuration;
    public float shieldCooldown;
    
    public float gunDamage;
    public float gunRate;
    
    public float abilityDamage;

    public bool UpgradedWeapon;
}
