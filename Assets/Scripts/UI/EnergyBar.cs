using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBarSlider;
    GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        energyBarSlider.maxValue = player.GetComponent<PlayerState>().maxEnergy;
    }

    private void Update()
    {
        energyBarSlider.value = player.GetComponent<PlayerState>().currentEnergy;
    }
}