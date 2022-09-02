using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[SerializeField] Animator m_animator;

	[SerializeField] Transform weaponPosition;

    [Header("Interval Between Bullets")]
	[SerializeField] float nextFire = 1f;                               // Time before next projectile is spawned 
	public float fireDelay = 0.5f;

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
	GameObject gameManager;
	GameObject upgradeManager;
	bool abilityLaser;

	void Awake()
    {
		controls = GameObject.FindGameObjectWithTag("Player");
		gameManager = GameObject.FindObjectOfType<GameManager>().gameObject;
		upgradeManager = GameObject.FindObjectOfType<UpgradeManager>().gameObject;
		pistol.transform.position = weaponPosition.transform.position;
		demonLaser.transform.position = weaponPosition.transform.position;

		shootWeapon = false;

		hasUpgradedWeapon = upgradeManager.GetComponent<UpgradeManager>().specialGunOn;
	}

    void Update()
    {
		if(Time.timeScale != 0)
        {
			abilityLaser = controls.GetComponentInChildren<PlayerAbilities>().abilityLaser;

			if (!hasUpgradedWeapon)
			{
				pistol.SetActive(true);
				demonLaser.SetActive(false);

				if (Input.GetMouseButtonDown(0) && Time.time > nextFire && !abilityLaser)
				{
					PistolShoot();
					shootWeapon = true;
				}
				else
					shootWeapon = false;
			}
			else if (hasUpgradedWeapon)
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
    }

	void PistolShoot()
    {
		m_animator.SetTrigger("Attack1");
		pistol.GetComponent<Pistol>().Shoot();
		nextFire = Time.time + fireDelay;
	}

	void DLaserShoot()
	{
		if (currentShotAmount != numberOfBullets)
		{
			m_animator.SetTrigger("Attack1");
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


    public void FireRateDecrease(float amount)
    {
		fireDelay -= amount;
	}
}
