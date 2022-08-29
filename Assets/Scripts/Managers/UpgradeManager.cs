using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour, IDataPersistance
{
    GameObject gameManager;
    GameObject player;
    GameObject pWeapon;
    
    [Header("Shield")]
    [Space]
    [SerializeField] float shieldCooldown;
    [SerializeField] float shieldDuration;

    [Header("Gun")]
    [Space]
    [SerializeField] float damageIncrease;
    public float gunDamage { get; private set; } = 0;

    [SerializeField] float gunShootingRate;

    [Header("Ability")]
    [Space]
    [SerializeField] float abilityDamageIncrease;
    public float abilityDamage { get; private set; } = 0;

    [Header("SpecialGunOn")]
    [Space]
    public bool specialGunOn;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (player == null && gameManager.GetComponent<GameManager>().inStartLevel)
        {
            player = GameObject.FindGameObjectWithTag("Player").gameObject;
            pWeapon = GameObject.FindGameObjectWithTag("PWeapon").gameObject;


            if (specialGunOn)
            {
                pWeapon.GetComponent<PlayerWeapon>().hasUpgradedWeapon = true;
            }
        }
    }

    public void UpdateSkill(int skillID)
    {
        //Shield Cooldown
        if (skillID >= 11 && skillID <= 13)
        {
            player.GetComponentInChildren<PlayerAbilities>().ShieldCooldownDecrease(shieldCooldown);
        }
        //Shield Duration
        else if (skillID >= 21 && skillID <= 23)
        {
            player.GetComponentInChildren<PlayerAbilities>().ShieldDurationIncrease(shieldDuration);
        }
        //Gun Damage
        else if (skillID >= 31 && skillID <= 33)
        {
           gunDamage += damageIncrease;
        }
        //Gun rate
        else if (skillID >= 41 && skillID <= 43)
        {
            pWeapon.GetComponent<PlayerWeapon>().FireRateDecrease(gunShootingRate);
        }
        //Ability Damage
        else if (skillID >= 51 && skillID <= 53)
        {
            abilityDamage += abilityDamageIncrease;
        }
        else if (skillID == 99)
        {
           specialGunOn = true;
           pWeapon.GetComponent<PlayerWeapon>().hasUpgradedWeapon = specialGunOn;
        }
    }

    public void LoadData(GameData data)
    {
        abilityDamage = data.abilityDamage;
        gunDamage = data.gunDamage;
        specialGunOn = data.UpgradedWeapon;
    }

    public void SaveData(GameData data)
    {
        data.abilityDamage = abilityDamage;
        data.gunDamage = gunDamage;
        data.UpgradedWeapon = specialGunOn;
    }
}
