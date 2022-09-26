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

    [SerializeField] GameObject storyMenu;

    UIManager uiManager;

    // Timer
    PlayerAbilities shield;

    [Header("Shield Duration")]
    [Space]
    [SerializeField] GameObject shieldDurationUI;
    [SerializeField] Text shieldDurationUITimer;

    [Header("Shield Cooldown")]
    [Space]
    [SerializeField] GameObject shieldCooldownUI;
    [SerializeField] Text shieldCooldownUITimer;

    private void Start()
    {
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");

        if(player != null)
        {
            healthBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;
            energyBarSlider.maxValue = player.GetComponent<PlayerStats>().maxHealth;

            updateSliders();

            updateShieldUI();
        }

        if(boss != null)
            BossUI();

        if (Input.GetKeyDown(KeyCode.P) && upgradePanel.active == false && storyMenu.active == false)
        {
            if (!isPaused)
            {
                PauseGame();
            }
        }

        if(Input.GetKeyDown(KeyCode.U) && pauseMenu.active == false && storyMenu.active == false)
        {
            TurnOnOrOFFPanel(true);
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
        uiManager.ResetKeyCards();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameObject.FindObjectOfType<ScenesManager>().gameObject.GetComponent<ScenesManager>().BringNextScene("MainMenu");
    }

    void updateShieldUI()
    {
        shield = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAbilities>();

        if (shield.shieldActive == true)
        {
            shieldDurationUI.SetActive(true);
            shieldDurationUITimer.text = "Duration: " + (int)shield.shieldDurationCounter;
        }
        else
            shieldDurationUI.SetActive(false);

        if (shield.shieldOnCooldown == true)
        {
            shieldCooldownUI.SetActive(true);
            shieldCooldownUITimer.text = "Cooldown: " + (int)shield.shieldCooldownCounter;
        }
        else
            shieldCooldownUI.SetActive(false);
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

    //Update boss health bar
    public void updateCanvasUI(Sprite characterProfile, bool isBossLevel)
    {
        this.bossLevel = isBossLevel;

        characterImage.GetComponent<Image>().sprite = characterProfile;
    }

    
    //Update Keycards and mutation points on the UI
    public void UpdatePlayerUI(int keys, int mutationPoints)
    {
        keycardsTextDisplay.text = keys.ToString();
        mutationPointsTextDisplay.text = mutationPoints.ToString();
        amountDisplay.text = mutationPoints.ToString();
    }

    //Turn on and off Upgrade Panel
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

