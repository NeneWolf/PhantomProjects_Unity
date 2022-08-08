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
	bool canShoot;

    private void Awake()
    {
		weapon = transform.GetComponentInParent<PlayerWeapon>();
    }

    private void Update()
	{
		if (!canShoot)
		{
			attackTimer -= Time.deltaTime;
		}
		if (attackTimer < 0)
        {
			attackTimer = 0;
			canShoot = true;
        }
	}

    #region Weapon Methods

	public void Shoot()
	{
		if (canShoot)
        {
			Instantiate(weaponProjectilePrefab, firePoint.position, firePoint.rotation);
			canShoot = false;
			attackTimer = weapon.pistolAttackInterval;
		}
	}

	#endregion
}
