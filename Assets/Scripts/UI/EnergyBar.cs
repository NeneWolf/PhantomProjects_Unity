using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energyBarSlider;
        
    public void SetMaxEnergy(int energy)
    {
        energyBarSlider.maxValue = energy;
        energyBarSlider.value = energy;
    }

    public void SetEnergy(int energy)
    {
        energyBarSlider.value = energy;
    }
}