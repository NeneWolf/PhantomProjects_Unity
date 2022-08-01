using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthBarSlider.maxValue = player.GetComponent<PlayerState>().maxHealth;
    }

    private void Update()
    {
        healthBarSlider.value = player.GetComponent<PlayerState>().currentHealth;
    }
}