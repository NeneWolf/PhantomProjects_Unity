using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonLaser : MonoBehaviour
{
    [Header("Projectile")]
    [Space]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject weaponProjectilePrefab;                       // Load projectile sprite

    [Header("Laser")]
    [SerializeField] GameObject laser;
    [SerializeField] float laserDuration = 3f;                               // How long the laser will stay active for

    float laserDurationCounter;                                                 // Variable to hold timer for the laser's duration

    void Awake()
    {
        laserDurationCounter = laserDuration;                                   // Set Timer
    }

    public void Shoot()
    {
        GameObject.Instantiate(weaponProjectilePrefab, firePoint.position, transform.rotation);
    }

    public void ShootLaser()
    {
        laser.SetActive(true);
        StartCoroutine(LaserDuration());
    }


    IEnumerator LaserDuration()
    {
        yield return new WaitForSecondsRealtime(laserDuration);                 // Leave the laser active for the specified amount of time
        laser.SetActive(false);                                       // Disable the line once the duration is up 
    }

}
