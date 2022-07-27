using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[Header("Player Weapon")]
	[Space]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject weaponProjectilePrefab;                       // Load projectile sprite
	[SerializeField] float attackInterval = 1f;                               // Time before next projectile is spawned 
	float attackTimer = 0;

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

    private void Attack()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
			attackTimer = attackInterval;                                       // Reset timer between each attack to prevent continous attack inputs
		}
	}

	void Shoot()
	{
		Instantiate(weaponProjectilePrefab, firePoint.position, firePoint.rotation);
	}

    #endregion
}
