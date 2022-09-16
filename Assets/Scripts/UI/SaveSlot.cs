using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] public Text levelCurrentlyIn;
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

            levelCurrentlyIn.text = GetSceneNameFromBuildIndex(data.currentLevelIndex);

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

            modeAndTimerCurrentlyIn.text = modeSelected; // TO BE WORKED ON TO EVENTUALLY ADD A TIMER :D KEK I COULDNT BE BOTHERED ! TO BE CONTINUED...
        }
    }

    static string GetSceneNameFromBuildIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public string GetLevelCurrentlyIn(GameData data)
    {
        return this.levelCurrentlyIn.text;
    }

    public void ResetSlot()
    {
        noDataContent.SetActive(true);
        hasDataContent.SetActive(false);
    }
}
