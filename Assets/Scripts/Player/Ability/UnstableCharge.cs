using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableCharge : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] Transform handLocation;
    [SerializeField] GameObject laser;
    [SerializeField] float laserDuration = 3f;                               // How long the laser will stay active for
    GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void ShootLaser()
    {
        laser.SetActive(true);
        StartCoroutine(LaserDuration());
    }

    IEnumerator LaserDuration()
    {
        yield return new WaitForSecondsRealtime(laserDuration);                 // Leave the laser active for the specified amount of time
        laser.SetActive(false);                                                 // Disable the line once the duration is up 
        player.GetComponentInChildren<PlayerAbilities>().abilityLaser = false;
    }
}
