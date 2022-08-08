using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonLaser : MonoBehaviour
{
    //[Header("Laser Stats")]
    //[Space]
    //[SerializeField] float rayDistance = 10;                                    // How far the laser will travel
    //[SerializeField] float laserDuration = 10.5f;                               // How long the laser will stay active for
    //[SerializeField] float laserTickRate = 0.2f;                                // How often the laser will be dealing damage
    //[SerializeField] float laserCooldown = 30.5f;                               // Cooldown for the laser

    //public Transform laserFirePoint;                                            // The position the laser will be firing from
    //public LineRenderer lineRenderer;                                           // Line that will be acting as the laser
    //float damage;
    //float laserDurationCounter;                                                 // Variable to hold timer for the laser's duration
    //float laserTickRateCounter;                                                 // Variable to hold timer for laser's tick rate
    //float laserCooldownCounter;                                                 // Variable to hold timer for laser's cooldown
    //bool laserReady = true;                                                     // Check to see if laser is ready
    //bool laserActive = false;                                                   // Check to see if the laser is currently active

    //PlayerControls playerDirection;


    //[Header("Target")]
    //[Space]
    //[SerializeField] LayerMask whatIsEnemy;                                     // Identify what an enemy is

    //private void Awake()
    //{
    //    damage = GetComponentInParent<PlayerWeapon>().laserDamage;
    //    playerDirection = GameObject.Find("Player").GetComponent<PlayerControls>();
    //}

    //private void Start()
    //{
    //    laserDurationCounter = laserDuration;                                   // Set Timer
    //    laserTickRateCounter = laserTickRate;                                   // Set Timer
    //    laserCooldownCounter = laserCooldown;                                   // Set Timer
    //}

    //private void Update()
    //{
    //    if (laserActive)                                                        // If the laser is currently active carry out the following methods
    //    {
    //        SetupLaser();
    //    }
    //}

    //public void ShootLaser()
    //{
    //    if (laserReady)
    //    {
    //        lineRenderer.enabled = true;
    //        laserActive = true;
    //        laserReady = false;

    //        StartCoroutine(LaserDuration());

    //        if (!laserReady && !laserActive)                                // Once the laser is no longer activate start its cooldown period
    //        {
    //            LaserCooldownTimer();                                       // Method for the laser's cooldown
    //        }
    //    }
    //}

    //void SetupLaser()
    //{
    //    AdjustLaserPosition();                                              // Adjust the direction of the laser
    //    DealDamage();                                                       // Deal damage with the laser whilst it's active
    //    LaserDurationTimer();                                               // Start the timer for the laser's duration
    //}

    //void DealDamage()
    //{
    //    RaycastHit2D enemyHit = Physics2D.Raycast(laserFirePoint.position, transform.right, rayDistance, whatIsEnemy);                  // Create a raycast to shoot forward an invisible laser to detect if an enemy is being hit

    //    laserTickRateCounter -= Time.deltaTime;                                 // Reduce the tick rate timer by real time seconds

    //    if (laserTickRateCounter < 0)                                           // Adjust timer to not go below 0 using the fix values method
    //        laserTickRateCounter = 0;

    //    if (enemyHit & laserTickRateCounter == 0)                               // If the raycast hits an object and the tick rate is 0
    //    {
    //        laserTickRateCounter = laserTickRate;                               // Reset the tick rate counter to prevent damage from occuring continously 
    //        enemyHit.collider.transform.parent.GetComponent<Entity>().Damage(damage);                  // Deal the laser's damage to the object hit if it's an enemy
    //    }
    //}

    //IEnumerator LaserDuration()
    //{
    //    yield return new WaitForSecondsRealtime(laserDuration);                 // Leave the laser active for the specified amount of time

    //    lineRenderer.enabled = false;                                         // Disable the line once the duration is up
    //    laserActive = false;                                                    // Set the laser active check to false to stop the previous methods
    //    laserCooldownCounter = laserCooldown;                                   // Reset the laser cooldown timer

    //    StartCoroutine(LaserCooldown());                                        // Start the laser's cooldown
    //}

    //IEnumerator LaserCooldown()
    //{
    //    yield return new WaitForSecondsRealtime(laserCooldown);                 // Wait for the specified amount of time, in this case the laser's cooldown

    //    laserReady = true;                                                      // Set the laser ready check to true after the cooldown period is over
    //    laserDurationCounter = laserDuration;                                   // Reset the laser duration timer
    //}

    //private void LaserDurationTimer()
    //{
    //    laserDurationCounter -= Time.deltaTime;                                 // Reduce the laser duration timer by real time seconds
    //}

    //private void LaserCooldownTimer()
    //{
    //    laserCooldownCounter -= Time.deltaTime;                                 // Reduce the laser cooldown timer by real time seconds
    //}

    //void Draw2DRay(Vector2 startPos, Vector2 endPos)                            // Method to draw the laser
    //{
    //    lineRenderer.SetPosition(0, startPos);                                // Set the laser's start position
    //    lineRenderer.SetPosition(1, endPos);                                  // Set the laser's end position
    //}

    //void AdjustLaserPosition()                                                  // Method to adjust the laser's directionality
    //{
    //    if (playerDirection.facingRight)                    // If the laser's position is greater than the firepoint...
    //    {
    //        Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x + rayDistance, laserFirePoint.position.y));            // Shoot the laser to the right
    //    }
    //    else
    //    {
    //        Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x - rayDistance, laserFirePoint.position.y));            // Else shoot the laser to the left
    //    }
    //}
}
