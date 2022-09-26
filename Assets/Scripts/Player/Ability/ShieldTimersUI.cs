using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShieldTimersUI : MonoBehaviour
{
    PlayerAbilities shield;

    [Header("Shield Duration")]
    [Space]
    [SerializeField] GameObject shieldDurationUI;
    [SerializeField] TextMeshProUGUI shieldDurationUITimer;

    [Header("Shield Cooldown")]
    [Space]
    [SerializeField] GameObject shieldCooldownUI;
    [SerializeField] TextMeshProUGUI shieldCooldownUITimer;

    private void Update()
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
}
