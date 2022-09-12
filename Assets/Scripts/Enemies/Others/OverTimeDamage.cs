using PhantomProjects.Core;
using PhantomProjects.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class OverTimeDamage : MonoBehaviour
{
    DifficultyManager difficultyManager;
    
    [Header("Active Wire (Default)")]
    [SerializeField] bool wire = true; //Default
    [SerializeField] bool canDamage = false;

    [Header("Toxic Area")]
    [SerializeField] bool toxicArea = false;

    [Header("Damage Overtime")]
    [SerializeField] float damage;
    [SerializeField] float damageRate;

    [Header("Other Details")]
    [SerializeField] LayerMask whatIsPlayer;
    [SerializeField] ParticleSystem sparks;


    Collider2D damageHit;
    float nextTimeToDamage = 0f;

    [Header("Wire Lights")]
    [SerializeField]Light2D light;

    public bool malfunctionLight;

    [SerializeField] float maxIntensity;
    [SerializeField] float minIntensity;
    [SerializeField] float secondsBetweenFlickers;

    float currentIntensity;

    void Start()
    {
        difficultyManager = GameObject.FindObjectOfType<DifficultyManager>().GetComponent<DifficultyManager>();
        damage *= difficultyManager.difficultyMultiplier;
        
        if (wire && canDamage)
        {
            sparks.Play();
        }

        //if (malfunctionLight)
        //{
        //    light = gameObject.GetComponent<Light2D>();
        //    currentIntensity = maxIntensity;
        //    light.intensity = currentIntensity;
        //    StartCoroutine(LightFlicker());
        //}
    }

    void Update()
    {
        damageHit = Physics2D.OverlapBox(transform.position, new Vector2(1.3f, 0.5f), 0f, whatIsPlayer);

        if (wire && canDamage)
        {
            if (damageHit)
            {
                if(Time.time > nextTimeToDamage)
                {
                    nextTimeToDamage = Time.time + damageRate;
                    damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                }
            }
        }

        if (toxicArea && damageHit)
        {
            if (Time.time > nextTimeToDamage)
            {
                nextTimeToDamage = Time.time + damageRate;
                damageHit.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }

    IEnumerator LightFlicker()
    {
        yield return new WaitForSeconds(secondsBetweenFlickers);
        light.intensity = Random.Range(minIntensity, maxIntensity);
        StartCoroutine(LightFlicker());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(1.3f, 0.5f));
    }

}
