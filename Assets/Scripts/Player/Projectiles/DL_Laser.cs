using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DL_Laser : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;                                   // Line that will be acting as the laser
    [SerializeField] Transform laserFirePoint;                                   // The position the laser will be firing from

    [Header("Laser Stats")]
    [Space]
    [SerializeField] float damage = 0.3f;

    [SerializeField] float rayDistance = 10;                                    // How far the laser will travel
    [SerializeField] float laserTickRate = 0.2f;                                // How often the laser will be dealing damage

    float laserTickRateCounter;                                                 // Variable to hold timer for laser's tick rate

    [Header("Target")]
    [Space]
    [SerializeField] LayerMask whatIsEnemy;                                     // Identify what an enemy is

    GameObject controls;


    void Awake()
    {
        controls = GameObject.FindGameObjectWithTag("Player");
        laserTickRateCounter = laserTickRate;                                   // Set Timer
    }


    void Update()
    {
        DealDamage();
        AdjustLaserPosition();
    }

    void DealDamage()
    {
        RaycastHit2D enemyHit = Physics2D.Raycast(laserFirePoint.position, transform.right, rayDistance, whatIsEnemy);                  // Create a raycast to shoot forward an invisible laser to detect if an enemy is being hit

        laserTickRateCounter -= Time.deltaTime;                                 // Reduce the tick rate timer by real time seconds

        if (laserTickRateCounter < 0)                                           // Adjust timer to not go below 0 using the fix values method
            laserTickRateCounter = 0;

        if (enemyHit & laserTickRateCounter == 0)                               // If the raycast hits an object and the tick rate is 0
        {
            laserTickRateCounter = laserTickRate;       // Reset the tick rate counter to prevent damage from occuring continously 
            if (enemyHit.collider.tag =="Enemy")
            {
               enemyHit.collider.transform.parent.GetComponent<Entity>().Damage(damage);                  // Deal the laser's damage to the object hit if it's an enemy
            }
            else if(enemyHit.collider.tag == "BMinion")
            {
                enemyHit.collider.gameObject.GetComponentInParent<MinionsControls>().TakeMinionDamage(enemyHit.collider.gameObject.name, damage);                  // Deal the laser's damage to the object hit if it's an enemy 
            }
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)                            // Method to draw the laser
    {
        lineRenderer.SetPosition(0, startPos);                                // Set the laser's start position
        lineRenderer.SetPosition(1, endPos);                                  // Set the laser's end position
    }

    void AdjustLaserPosition()                                                  // Method to adjust the laser's directionality
    {
        if (controls.GetComponent<PrototypeHero>().m_facingDirection == 1)                    // If the laser's position is greater than the firepoint...
        {
            Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x + rayDistance, laserFirePoint.position.y));            // Shoot the laser to the right
        }
        else
        {
            Draw2DRay(laserFirePoint.position, new Vector2(laserFirePoint.position.x - rayDistance, laserFirePoint.position.y));            // Else shoot the laser to the left
        }
    }


}
