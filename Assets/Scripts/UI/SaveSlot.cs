using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private Text levelCurrentlyIn;
    [SerializeField] private Text modeAndTimerCurrentlyIn;

    string modeSelected;

    public void SetData(GameData data)
    {
        if(data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            levelCurrentlyIn.text = "Level " + data.currentLevelIndex;

            switch (data.modeSelected)
            {
                case 0:
                    modeSelected = "Easy";
                    break;
                case 1:
                    modeSelected = "Normal";
                    break;
                case 2:
                    modeSelected = "Nightmare";
                    break;
            }

            modeAndTimerCurrentlyIn.text = modeSelected + " | " + "00:00 Time"; // TO BE WORKED ON ...
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

}
