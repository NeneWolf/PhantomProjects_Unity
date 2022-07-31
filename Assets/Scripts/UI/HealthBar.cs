using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhantomProjects.Core;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;

    }

    public void Update()
    {
        healthBarSlider.value = player.GetComponent<PlayerStats>().currentHealth;
    }
}