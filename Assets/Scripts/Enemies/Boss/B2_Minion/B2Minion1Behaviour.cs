using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion1Behaviour : MonoBehaviour
{
    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }

    [Header("Parent || Child")]
    [SerializeField] GameObject boss;
    [SerializeField] GameObject laser;

    [Header("Fire - Laser")]
    [SerializeField] float warningTime;
    [SerializeField] float speed;
    float timeStamp;
    //public Vector3 originalAngle { get; private set;}
    Vector3 currentAngle;

    Vector3 targetAngle = new Vector3(0f, 0f, -65f);

    bool hasShoot = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (boss.GetComponent<Entity>().CheckPlayerInMinAgroRange() && !isDead)
        {
            currentAngle = transform.eulerAngles;
            RotateToAttack();
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    void RotateToAttack()
    {
        if (!hasShoot)
        {
            //print("Go Down");
            var step = speed * Time.deltaTime;

            if (boss.GetComponent<Entity>().facingDirection == 1)
            {
                currentAngle = new Vector3(
                    Quaternion.identity.x,
                    Quaternion.identity.y,
                    Mathf.LerpAngle(currentAngle.z, targetAngle.z, step));

                transform.eulerAngles = currentAngle;
            }
            else if (boss.GetComponent<Entity>().facingDirection == -1)
            {

                currentAngle = new Vector3(
                        Quaternion.identity.x,
                        Quaternion.identity.y - 180,
                        Mathf.LerpAngle(currentAngle.z, targetAngle.z, step));

                transform.eulerAngles = currentAngle;
            }

            StartCoroutine(ShootTimer());
        }
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(warningTime);
        hasShoot = true;
        laser.SetActive(true);

        StopCoroutine(ShootTimer());
    }

    public void SetFire(bool fire)
    {
        hasShoot = fire;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PBullet")
        {
            //TODO: Add Damage from the player
            //TakeDamage()
        }
    }

    void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}
