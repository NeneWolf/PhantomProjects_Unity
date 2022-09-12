using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2Minion1Behaviour : MonoBehaviour
{
    [SerializeField] AudioSource m_audio;
    
    [Header("Details")]
    [SerializeField] float maxHealth;
    float currentHealth;
    public bool isDead { get; private set; }
    [SerializeField] ParticleSystem bloodEffect;

    [Header("Parent || Child")]
    [SerializeField] GameObject boss;
    [SerializeField] GameObject laser;

    [Header("Fire - Laser")]
    [SerializeField] float warningTime;
    [SerializeField] float speed;
    float timeStamp;

    Vector3 currentAngle;
    Vector3 targetAngle = new Vector3(0f, 0f, -65f);
    Vector3 originalAngle = new Vector3(0f, 0f, 0f);
    bool rotateToAttack = false;
    bool rotateToOrigin = false;

    Animator animation;

    private void Awake()
    {
        currentHealth = maxHealth;
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        if (rotateToAttack)
        {
            RotateToAttack();
        }
        else if (rotateToOrigin)
        {
            RotateToOrigin();
        }
    }

    void RotateToAttack()
    {
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

        StartCoroutine(WaitRotateToOrigin());
    }

    void RotateToOrigin()
    {
        laser.SetActive(true);
        var step = speed * Time.deltaTime;

        if (boss.GetComponent<Entity>().facingDirection == 1)
        {
            currentAngle = new Vector3(
                    Quaternion.identity.x,
                    Quaternion.identity.y,
                    Mathf.LerpAngle(currentAngle.z, originalAngle.z, step));
            transform.eulerAngles = currentAngle;
        }
        else if (boss.GetComponent<Entity>().facingDirection == -1)
        {
            currentAngle = new Vector3(
                    Quaternion.identity.x,
                    Quaternion.identity.y - 180,
                    Mathf.LerpAngle(currentAngle.z, originalAngle.z, step));

            transform.eulerAngles = currentAngle;
        }

        StartCoroutine(InOrigin());
    }

    IEnumerator WaitRotateToOrigin()
    {
        yield return new WaitForSeconds(3f);
        rotateToAttack = false;
        rotateToOrigin = true;
    }

    IEnumerator InOrigin()
    {
        yield return new WaitForSeconds(3f);
        laser.SetActive(false);
        rotateToOrigin = false;
    }

    //IEnumerator ShootTimer()
    //{
    //    yield return new WaitForSeconds(warningTime);
    //    hasShoot = true;
    //    laser.SetActive(true);

    //    StopCoroutine(ShootTimer());
    //}

    public void SetFire()
    {
        rotateToAttack = true;
    }

    public void TakeDamage(float dmg)
    {
        m_audio.Play();
        
        GameObject.Instantiate(bloodEffect, transform.position, Quaternion.identity);

        if (currentHealth - dmg <= 0)
        {
            currentHealth = 0;
            isDead = true;
            StartCoroutine(WaitToDie());
        }
        else
        {
            currentHealth -= dmg;
        }
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);

    }
}
