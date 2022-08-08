using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
	[Header("Projectile")]
	[Space]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject weaponProjectilePrefab;                       // Load projectile sprite

	PlayerWeapon weapon;
	float attackTimer = 0;

    private void Awake()
    {
		weapon = transform.GetComponentInParent<PlayerWeapon>();
    }

    private void Update()
	{
		if (attackTimer <= 0)
		{
			attackTimer = 0;                                                    // Set attack timer to 0 instead of going into negatives
			Attack();                                                           // Player Attack Method
		}
		else
		{
			attackTimer -= Time.deltaTime;                                      // Recude attack timer every second so the next attack can be peformed
		}
	}

    #region Weapon Methods

    public void Attack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
			attackTimer = weapon.pistolAttackInterval;                                       // Reset timer between each attack to prevent continous attack inputs
		}
	}

	void Shoot()
	{
		Instantiate(weaponProjectilePrefab, firePoint.position, firePoint.rotation);
	}

	#endregion
}
