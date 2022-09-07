using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlotMenu : MonoBehaviour
{
    private SaveSlot[] saveSlots;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    private void Start()
    {
        ActivateMenu();
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
        }
    }


    public void Refresh()
    {
        ActivateMenu();
    }
}
