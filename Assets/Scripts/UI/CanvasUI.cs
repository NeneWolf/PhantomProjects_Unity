using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PhantomProjects.Managers;

public class CanvasUI : MonoBehaviour
{
    // if it has red - It works, just visual studio refuses to bring the right files dw...
    [Header("UI Text Fields")]
    [Space]
    [SerializeField] Image characterImage;

    [SerializeField] Text keycardsTextDisplay;
    [SerializeField] Text mutationPointsTextDisplay;
    [SerializeField] Text bossName;

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
    bool bossLevel = false;

    //Upgrade Panel
    [Header("Upgrade Menu")]
    [Space]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] Text amountDisplay;

    [Header("Pause Menu")]
    [Space]
    [SerializeField] GameObject pauseMenu;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");

        if(player != null)
        {
            healthBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
            energyBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;

            updateSliders();
        }

        if(boss != null)
            BossUI();

        if (Input.GetKeyDown(KeyCode.P) && upgradePanel.activeInHierarchy == false)
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        DataPersistanceManager.instanceData.SaveGame();
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject.FindObjectOfType<ScenesManager>().gameObject.GetComponent<ScenesManager>().BringNextScene("MainMenu");
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

    public void updateCanvasUI(Sprite characterProfile, bool isBossLevel)
    {
        this.bossLevel = isBossLevel;

        characterImage.GetComponent<Image>().sprite = characterProfile;
    }

    public void UpdatePlayerUI(int keys, int mutationPoints)
    {
        keycardsTextDisplay.text = keys.ToString();
        mutationPointsTextDisplay.text = mutationPoints.ToString();
        amountDisplay.text = mutationPoints.ToString();
    }

    public void TurnOnOrOFFPanel(bool value)
    {
        upgradePanel.SetActive(value);

        if (value)
        {
            Time.timeScale = 0f;
        }
        else
            Time.timeScale = 1f;
    }
}

