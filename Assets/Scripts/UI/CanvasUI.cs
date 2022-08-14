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

    public Slider healthBarSlider;
    public Slider energyBarSlider;

    //For Pause menu
    bool isPaused;

    //Player information
    GameObject player;
    bool isDead;

    [Header("Boss")]
    [Space]
    [SerializeField] GameObject bossHealthBar;
    GameObject boss;
    bool bossLevel;

    GameObject sceneManager;


    public void updateCanvasUI(GameObject player, GameObject boss, Sprite characterProfile)
    {
        this.player = player;
        this.boss = boss;

        characterImage.GetComponent<Image>().sprite = characterProfile;

        if (boss != null)
        {
            bossLevel = true;
            bossName.text = boss.GetComponent<Entity>().name.ToString();

            bossHealthBar.SetActive(bossLevel);
        }
        else
        {
            if (boss.GetComponent<Entity>().currentHealth <= 0 || boss == null)
            {
                bossHealthBar.SetActive(false);
            }
        }
    }

    void updateSliders()
    {

        healthBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
        healthBarSlider.value = player.GetComponent<PlayerStats>().currentHealth;

        energyBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
        energyBarSlider.value = player.GetComponent<PlayerStats>().currentHealth;

    }

    public void UpdatePlayerUI(int keys, int mutationPoints)
    {
        keycardsTextDisplay.text = keys.ToString();
        mutationPointsTextDisplay.text = mutationPoints.ToString();
    }
}

