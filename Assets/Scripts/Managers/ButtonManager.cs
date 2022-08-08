using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] menus; // 0 - Main Menu //1 - Select Slot // 2 - Mode
    [SerializeField] GameObject pCapsule;

    public void MoveToSelectMenu()
    {
        menus[1].SetActive(true);
        menus[0].SetActive(false);
        pCapsule.GetComponent<Capsule>().TurnOnFog();
    }

    public void MoveToModeMenu()
    {
        menus[2].SetActive(true);
        menus[1].SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        pCapsule.GetComponent<Capsule>().TurnOffFog();
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }

        menus[0].SetActive(true);
    }
}
