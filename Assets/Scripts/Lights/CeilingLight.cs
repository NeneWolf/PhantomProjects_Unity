using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering.Universal;

public class CeilingLight : MonoBehaviour
{
    public bool malfunctionLight;

    [SerializeField] float maxIntensity;
    [SerializeField] float minIntensity;
    [SerializeField] float secondsBetweenFlickers;
    Light2D light;

    float currentIntensity;
    


    // Start is called before the first frame update
    void Start()
    {
        if (malfunctionLight)
        {
            light = GetComponent<Light2D>();
            currentIntensity = maxIntensity;
            light.intensity = currentIntensity;
            StartCoroutine(LightFlicker());
        }

    }

    IEnumerator LightFlicker()
    {
        yield return new WaitForSeconds(secondsBetweenFlickers);
        light.intensity = Random.Range(minIntensity, maxIntensity);
        StartCoroutine(LightFlicker()); 
    }

}
