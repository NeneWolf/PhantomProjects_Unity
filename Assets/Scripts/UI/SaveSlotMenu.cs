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

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        DataPersistanceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
    }

    public void OnCleanClick(SaveSlot saveSlot)
    {
        DataPersistanceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
    }

    public void ActivateMenu()
    {
        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instance.GetAllProfileGameData();

        foreach(SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);

            saveSlot.SetData(profileData);
        }
    }

    private void Start()
    {
        ActivateMenu();
    }

    public void Refresh()
    {
        ActivateMenu();
    }
}
