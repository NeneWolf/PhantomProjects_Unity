using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PhantomProjects.Core;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBarSlider;
    GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        energyBarSlider.maxValue = player.GetComponent<PlayerStats>().maxEnergy;
    }

    public void Update()
    {
        energyBarSlider.value = player.GetComponent<PlayerStats>().currentEnergy;
    }
}