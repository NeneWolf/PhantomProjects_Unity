using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] Animator m_animator;

	[SerializeField] Transform weaponPosition;

    [Header("Interval Between Bullets")]
	[SerializeField] float nextFire = 1f;                               // Time before next projectile is spawned 
	[SerializeField] float fireDelay = 0.5f;

	[Header("Pistol")]
	[Space]
	[SerializeField] GameObject pistol;

	public bool hasUpgradedWeapon;
	

    [Header("Demon Laser")]
    [Space]
    [SerializeField] GameObject demonLaser;
	[SerializeField] int numberOfBullets = 3;
	int currentShotAmount = 0;

	public bool shootWeapon { get; private set; }
	GameObject controls;
	bool abilityLaser;

	void Awake()
    {
		controls = GameObject.FindGameObjectWithTag("Player");
		pistol.transform.position = weaponPosition.transform.position;
		demonLaser.transform.position = weaponPosition.transform.position;

		shootWeapon = false;
	}

    void Update()
    {
		abilityLaser = controls.GetComponent<PlayerAbilities>().abilityLaser;

		if (!hasUpgradedWeapon)
        {
			pistol.SetActive(true);
			demonLaser.SetActive(false);

			if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !abilityLaser)
			{

				m_animator.SetTrigger("Attack1");
				PistolShoot();
				shootWeapon = true;
			}
			else
				shootWeapon = false;
		}
		else if(hasUpgradedWeapon)
        {
			pistol.SetActive(false);
			demonLaser.SetActive(true);

			if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !abilityLaser)
			{
				DLaserShoot();
				shootWeapon = true;

			}
			else
				shootWeapon = false;
		}
    }

	void PistolShoot()
    {
		pistol.GetComponent<Pistol>().Shoot();
		nextFire = Time.time + fireDelay;
	}

	void DLaserShoot()
	{
		if(currentShotAmount != numberOfBullets)
        {
			currentShotAmount++;
			demonLaser.GetComponent<DemonLaser>().Shoot();
			nextFire = Time.time + fireDelay;
		}
        else
        {
			currentShotAmount = 0;
			demonLaser.GetComponent<DemonLaser>().ShootLaser();
			nextFire = Time.time + fireDelay;
		}
	}


	//   public void WeaponDamageIncrease(float amount)                                    // Upgrade (Increase) the player's weapon damage
	//{
	//	pistolDamage += amount;
	//}

	//public void FireRateIncrease(float amount)
	//   {
	//	if (pistolAttackInterval - amount <= pistolMinimumAttackInterval)
	//	{
	//		pistolAttackInterval = pistolMinimumAttackInterval;
	//	}
	//	else pistolAttackInterval -= amount;
	//   }


}
