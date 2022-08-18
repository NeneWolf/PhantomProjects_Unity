using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using PhantomProjects.Managers;

public class DataPersistanceManager : MonoBehaviour
{
    GameObject sceneManager;
    public int lastLevelIndex { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjects;
    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance { get; private set; }

    private string selectedProfileId = "";

    private void Awake()
    {
        instance = this;
        sceneManager = GameObject.Find("SceneManager");
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    // Saves on every loaded scene
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        //SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);

        // if no data can be loaded, initialize to a new game
        if (this.gameData == null)
            NewGame();


        foreach(IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.LoadData(gameData);
        }
        
        lastLevelIndex = gameData.currentLevelIndex;

    }


    public void SaveGame()
    {
        foreach (IDataPersistance dataPersistanceObj in dataPersistanceObjects)
        {
            dataPersistanceObj.SaveData(gameData);
        }

        dataHandler.Save(gameData, selectedProfileId);
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public Dictionary<string, GameData> GetAllProfileGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
