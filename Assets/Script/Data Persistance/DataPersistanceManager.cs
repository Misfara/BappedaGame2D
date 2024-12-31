using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine.SceneManagement;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableDataPersistance = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overrideSelectedProfileId = false;
    [SerializeField] private string testSelectedProfileId="test";

    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    [Header("AutoSavingConfiguration")]
    [SerializeField] private float autoSaveTimeSeconds= 300f;

    private FileDataHandler dataHandler;

    private string selectedProfileId = "";

    private Coroutine autoSaveCoroutine;
    private GameData gameData;
    public bool isNewGame = false;
    private List<IDataPersistence> dataPersistenceObjects;
    public static DataPersistenceManager instance { get; private set;}

  private void Awake()
    {
        if(instance != null)
        {
             Debug.Log("Found more than one persistence data in the scene ,destorying the newest one");
             Destroy(this.gameObject);
             return;

        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        if(disableDataPersistance)
        {
            Debug.LogWarning("Data Persistance is currently disabled");
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName,useEncryption);

         InitializeSelectedProfileId();
    }

    private void OnEnable()
    {
         SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

    private void OnDisable()
    {
            SceneManager.sceneLoaded -= OnSceneLoaded;
         
    }
    public void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();

        if(autoSaveCoroutine != null)
        {
            StopCoroutine(autoSaveCoroutine);
        }
        autoSaveCoroutine = StartCoroutine(AutoSave());
    }


    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId  = newProfileId;
        LoadGame();
    }


    public void DeleteProfileData(string profileId)
    {
        dataHandler.Delete(profileId);
        InitializeSelectedProfileId();
        LoadGame();
    }

    private void InitializeSelectedProfileId()
    {
        

         this.selectedProfileId = dataHandler.GetMostRecentlyUpdatedProfileId();
         if(overrideSelectedProfileId)
         {
            this.selectedProfileId=testSelectedProfileId;
            Debug.LogWarning("Override  selected profile id with test id" + testSelectedProfileId);
         }
    }
    public void NewGame()
    {
        this.gameData = new GameData();
        isNewGame = true;
    }
    
    public void LoadGame()
    {

        if(disableDataPersistance)
        {
            return;
        }
        this.gameData = dataHandler.Load(selectedProfileId);

        if(this.gameData == null && initializeDataIfNull)
        {
            NewGame();
        }
        if(this.gameData == null)
        {
            Debug.Log("No Data Was Found");
            return; 
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }

        Debug.Log("Loaded gold count ="+" " + gameData.totalGold );
    }

    public void SaveGame()
    {

         if(disableDataPersistance)
        {
            return;
        }
        if(this.gameData == null)
        {
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        gameData.lastUpdated = System.DateTime.Now.ToBinary();
        
        dataHandler.Save(gameData,selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
       
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
        FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();

         return new List<IDataPersistence>(dataPersistenceObjects);

    }

    public Dictionary<String,GameData>GetAllProfilesGameData()
    {
        return dataHandler.LoadAllProfiles();
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    private IEnumerator AutoSave()
    {
        while(true)
        {
            yield return new WaitForSeconds(autoSaveTimeSeconds);
            SaveGame();
            Debug.Log("AutoSavedGame");
        }
    }

    public GameData GetGameData()
{
    return gameData;
}
}
