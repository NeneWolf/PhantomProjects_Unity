using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasUI : MonoBehaviour
{
    // if it has red - It works, just visual studio refuses to bring the right files dw...
    [Header("UI Text Fields")]
    [Space]
    [SerializeField] Image characterImage;

    [SerializeField] TextMeshProUGUI keycardsTextDisplay;
    [SerializeField] TextMeshProUGUI mutationPointsTextDisplay;
    [SerializeField] TextMeshProUGUI bossName;

    [Header("Player UI")]
    public Slider healthBarSlider;
    public Slider energyBarSlider;

    //For Pause menu
    bool isPaused;

    //Player information
    GameObject player;
    bool isDead;

    [Header("Boss UI")]
    [Space]
    [SerializeField] GameObject bossHealthBar;
    GameObject boss;
    bool bossLevel;

    private void Update()
    {
        updateSliders();
    }
    
    //Player Sliders
    void updateSliders()
    {
        healthBarSlider.value = player.GetComponent<PlayerStats>().currentHealth;
        energyBarSlider.value = player.GetComponent<PlayerStats>().currentEnergy;
    }

    void BossUI()
    {
        //Boss
        if (bossLevel)
        {
            bossName.text = boss.GetComponent<Entity>().name.ToString();
            bossHealthBar.SetActive(bossLevel);

            if (boss.GetComponent<Entity>().currentHealth <= 0 || boss == null)
            {
                bossHealthBar.SetActive(false);
            }
        }
    }

    public void updateCanvasUI(GameObject player, GameObject boss, Sprite characterProfile, bool isBossLevel)
    {
        this.player = player;
        this.boss = boss;
        this.bossLevel = isBossLevel;

        characterImage.GetComponent<Image>().sprite = characterProfile;

        if (player != null)
        {
            healthBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
            energyBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
        }

        BossUI();
    }

    public void UpdatePlayerUI(int keys, int mutationPoints)
    {
        keycardsTextDisplay.text = keys.ToString();
        mutationPointsTextDisplay.text = mutationPoints.ToString();
    }
}

