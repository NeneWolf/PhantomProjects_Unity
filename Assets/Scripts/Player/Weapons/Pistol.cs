using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
	[Header("Projectile")]
	[Space]
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject weaponProjectilePrefab;                       // Load projectile sprite

	public void Shoot()
	{
		GameObject.Instantiate(weaponProjectilePrefab, firePoint.position, transform.rotation);
	}
}
