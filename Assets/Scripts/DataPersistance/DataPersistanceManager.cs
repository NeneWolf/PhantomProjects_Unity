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

    private string selectedProfileId = "";

    public static DataPersistanceManager instanceData { get; private set; }

    private void Awake()
    {
        if (instanceData != null)
        {
            Debug.Log("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instanceData = this;
        DontDestroyOnLoad(this.gameObject);
        
        sceneManager = GameObject.Find("SceneManager");

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistenceObjects();
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


    public void ChangeSelectedProfileId(string newProfileId)
    {
        // update the profile to use for saving and loading
        this.selectedProfileId = newProfileId;
        // load the game, which will use that profile, updating our game data accordingly
        LoadGame();
    }
    

    public void LoadGame()
    {
        // load any saved data from a file using the data handler
        this.gameData = dataHandler.Load(selectedProfileId);

        // start a new game if the data is null and we're configured to initialize data for debugging purposes
        if (this.gameData == null)
        {
            NewGame();
        }

        // if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // push the loaded data to all other scripts that need it
        foreach (IDataPersistance dataPersistenceObj in dataPersistanceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        
        lastLevelIndex = gameData.currentLevelIndex;
    }


    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // pass the data to other scripts so they can update it
        foreach (IDataPersistance dataPersistenceObj in dataPersistanceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // save that data to a file using the data handler
        dataHandler.Save(gameData, selectedProfileId);
    }

    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
        LoadGame();
    }

    private List<IDataPersistance> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistenceObjects);
    }

    public Dictionary<string, GameData> GetAllProfileGameData()
    {
        return dataHandler.LoadAllProfiles();
    }
}
