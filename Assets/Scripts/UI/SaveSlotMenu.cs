using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotMenu : MonoBehaviour
{
    private SaveSlot[] saveSlots;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
        ActivateMenu();
    }

    private void Start()
    {
        
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DataPersistanceManager.instanceData.ChangeSelectedProfileId(saveSlot.GetProfileId());
        //DataPersistanceManager.instanceData.SaveGame();
    }

    public void OnCleanClick(SaveSlot saveSlot)
    {
        DataPersistanceManager.instanceData.DeleteProfileData(saveSlot.GetProfileId());
    }

    public void ActivateMenu()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instanceData.GetAllProfileGameData();

        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);

            if (saveSlot.GetLevelCurrentlyIn(profileData) == "Character Selection")
            {
                DataPersistanceManager.instanceData.DeleteProfileData(saveSlot.GetProfileId());
                saveSlot.ResetSlot();
            }
        }
    }


    public void Refresh()
    {
        ActivateMenu();
    }
}
