using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //TODO - Load any saved data from a file using the data handler
        // if no data can be loaded, initialize to a new game
        if (this.gameData == null)
            NewGame();
    }


    public void SaveGame()
    {

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
