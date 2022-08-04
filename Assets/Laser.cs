using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [Header("Laser Stats")]
    [Space]
    [SerializeField] int laserDamage = 5;                                       // Damage the laser will be dealing
    [SerializeField] float rayDistance = 10;                                    // How far the laser will travel
    [SerializeField] float laserDuration = 10.5f;                               // How long the laser will stay active for
    [SerializeField] float laserTickRate = 0.2f;                                // How often the laser will be dealing damage
    [SerializeField] float laserCooldown = 30.5f;                               // Cooldown for the laser

    float laserDurationCounter;                                                 // Variable to hold timer for the laser's duration
    float laserTickRateCounter;                                                 // Variable to hold timer for laser's tick rate
    float laserCooldownCounter;                                                 // Variable to hold timer for laser's cooldown
    bool laserReady = true;                                                     // Check to see if laser is ready
    bool laserActive = false;                                                   // Check to see if the laser is currently active
    public Transform laserFirePoint;                                            // The position the laser will be firing from
    public LineRenderer m_lineRenderer;                                         // Line that will be acting as the laser

    [Header("Target")]
    [Space]
    [SerializeField] LayerMask whatIsEnemy;                                     // Identify what an enemy is

    private void Start()
    {
        laserDurationCounter = laserDuration;                                   // Set Timer
        laserTickRateCounter = laserTickRate;                                   // Set Timer
        laserCooldownCounter = laserCooldown;                                   // Set Timer
    }

    private void Update()
    {
        ShootLaser();                                                           // Method to fire laser
    }

    void ShootLaser()
    {
        if (laserReady)                                                         // Check to see if the laser is ready to be used
        {
            if (Input.GetKeyDown(KeyCode.R))                                    // If the specified key is pressed, activate the laser
            {
                m_lineRenderer.enabled = true;                                  // Enable the line that will act as the laser
                laserActive = true;                                             // Set laser active check to true as laser is currently active
                laserReady = false;                                             // Set laser ready check to false as we don't want repeated activations of the laser

                StartCoroutine(LaserDuration());                                // Start the duration for the laser
            }
            else if (!laserReady && !laserActive)                               // Once the laser is no longer activate start its cooldown period
            {
                LaserCooldownTimer();                                           // Method for the laser's cooldown
            }
        }

        if (laserActive)                                                        // If the laser is currently active carry out the following methods
        {
            AdjustLaserPosition();                                              // Adjust the direction of the laser
            DealDamage();                                                       // Deal damage with the laser whilst it's active
            LaserDurationTimer();                                               // Start the timer for the laser's duration
        }
    }

    IEnumerator LaserDuration()
    {
        yield return new WaitForSecondsRealtime(laserDuration);                 // Leave the laser active for the specified amount of time

        m_lineRenderer.enabled = false;                                         // Disable the line once the duration is up
        laserActive = false;                                                    // Set the laser active check to false to stop the previous methods
        laserCooldownCounter = laserCooldown;                                   // Reset the laser cooldown timer

        StartCoroutine(LaserCooldown());                                        // Start the laser's cooldown
    }

    IEnumerator LaserCooldown()
    {
        yield return new WaitForSecondsRealtime(laserCooldown);                 // Wait for the specified amount of time, in this case the laser's cooldown

        laserReady = true;                                                      // Set the laser ready check to true after the cooldown period is over
        laserDurationCounter = laserDuration;                                   // Reset the laser duration timer
    }

    private void LaserDurationTimer()
    {
        laserDurationCounter -= Time.deltaTime;                                 // Reduce the laser duration timer by real time seconds
        FixValues(laserDurationCounter);                                        // Adjust timer to not go below 0 using the fix values method
    }

    private void LaserCooldownTimer()
    {
        laserCooldownCounter -= Time.deltaTime;                                 // Reduce the laser cooldown timer by real time seconds
        FixValues(laserCooldownCounter);                                        // Adjust timer to not go below 0 using the fix values method
    }

    private void FixValues(float timer)
    {
        if (timer < 0)                                                          // If the timer passed through this method goes below 0 set the timer to 0 to avoid negative numbers
        {
            timer = 0;
        }
    }

    void DealDamage()
    {
        RaycastHit2D enemyHit = Physics2D.Raycast(laserFirePoint.position, transform.right, rayDistance, whatIsEnemy);                  // Create a raycast to shoot forward an invisible laser to detect if an enemy is being hit

        laserTickRateCounter -= Time.deltaTime;                                 // Reduce the tick rate timer by real time seconds

        if (laserTickRateCounter < 0)                                           // Adjust timer to not go below 0 using the fix values method
            laserTickRateCounter = 0;

        if (enemyHit & laserTickRateCounter == 0)                               // If the raycast hits an object and the tick rate is 0
        {
            laserTickRateCounter = laserTickRate;                               // Reset the tick rate counter to prevent damage from occuring continously 
            enemyHit.collider.transform.parent.GetComponent<Entity>().Damage(laserDamage);                  // Deal the laser's damage to the object hit if it's an enemy
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)                            // Method to draw the laser
    {
        m_lineRenderer.SetPosition(0, startPos);                                // Set the laser's start position
        m_lineRenderer.SetPosition(1, endPos);                                  // Set the laser's end position
    }

    void AdjustLaserPosition()                                                  // Method to adjust the laser's directionality
    {
        if (m_lineRenderer.transform.position.x < laserFirePoint.position.x)                    // If the laser's position is greater than the firepoint...
        {
            Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x + rayDistance, laserFirePoint.position.y));            // Shoot the laser to the right
        }
        else
        {
            Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x - rayDistance, laserFirePoint.position.y));            // Else shoot the laser to the left
        }
    }
}
