using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
	[Header("Player Weapon - Pistol")]
	[Space]
	[SerializeField] public float pistolDamage = 20f;
	[SerializeField] public float pistolAttackInterval = 1f;                               // Time before next projectile is spawned 
	[SerializeField] float pistolMinimumAttackInterval = 0.5f;
	[SerializeField] Transform weaponPosition;

	[Header("Player Weapon - Demon Laser")]
	[Space]
	[SerializeField] public Sprite demonLaserSprite;
	[SerializeField] public float laserDamage;
	[SerializeField] public float laserAttackInterval;
	[SerializeField] public float laserMinimumAttackInterval;

    private void Awake()
    {
		transform.position = weaponPosition.position;
    }

    #region Upgrade Methods

    public void WeaponDamageIncrease(float amount)                                    // Upgrade (Increase) the player's weapon damage
	{
		pistolDamage += amount;
	}

	public void FireRateIncrease(float amount)
    {
		if (pistolAttackInterval - amount <= pistolMinimumAttackInterval)
		{
			pistolAttackInterval = pistolMinimumAttackInterval;
		}
		else pistolAttackInterval -= amount;
    }

	#endregion
}
