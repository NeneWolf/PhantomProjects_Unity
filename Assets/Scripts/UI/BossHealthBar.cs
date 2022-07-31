using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider bossHealthSlider;
    GameObject boss;

    // Start is called before the first frame update
    void Awake()
    {
        if (this.isActiveAndEnabled)
        {
            boss = GameObject.FindGameObjectWithTag("Boss");
            bossHealthSlider.maxValue = boss.GetComponent<Entity>().entityData.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bossHealthSlider.value = boss.GetComponent<Entity>().currentHealth;

    }
}
