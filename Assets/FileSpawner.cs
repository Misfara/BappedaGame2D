using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FileSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] filePrefabs;
    [SerializeField] public Button actionButton; // Reference to the button

    [Header("Attributes")]
    [SerializeField] private int baseFile = 4;
    [SerializeField] private float filePerSecond = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;

    [SerializeField] private float difficultyScalingFactor = 0.75f;
    [SerializeField] private TextMeshProUGUI fileName;
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int fileAlive;
    private int fileLeftToSpawn ;
    public bool isSpawning = false;
    FileMovement fileMovement;
   public int fileCounter = 1;
     public int fileCounterMax = 5;

    public bool playerCanMove =true;
    Animator player;
    private List<GameObject> spawnedFiles = new List<GameObject>();

    public bool changeUI;
    CheckingButton checkingButton;
    public GameObject spawnedFile;

    bool canClick =true;
    public bool isAccepted =false;

  private Coroutine spawningCoroutine;

    public void Start()
    {
       playerCanMove = true;
       changeUI = false;
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
       checkingButton = FindObjectOfType<CheckingButton>();
       actionButton.interactable = false;
    }

    public void Update()
    {
       if(isSpawning== true)
       { 
        if(fileCounter > fileCounterMax)
        {
                fileCounter = fileCounterMax;
        }

       }
    }

    public void StartWave()
    {
        if(isSpawning == true)
        {
        playerCanMove = false;
        player.SetBool("Run",false);
       
        fileLeftToSpawn = FilePerWave();
        
       if (spawningCoroutine != null)
        {
            StopCoroutine(spawningCoroutine);
        }

        spawningCoroutine = StartCoroutine(HandleSpawning());
    }
    }

    private IEnumerator HandleSpawning()
{
    if(isSpawning == true){
    while (true) // Infinite spawning loop
    {
        if (fileLeftToSpawn > 0 && !GameObjectExists("path5"))
        {
            yield return new WaitForSeconds(3f);

            SpawnFile();
            fileLeftToSpawn--;
            fileAlive++;
        }
        else if (GameObjectExists("path5"))
        {
            Debug.Log("Spawning paused. Waiting for path5 to be destroyed.");
            yield return new WaitUntil(() => !GameObjectExists("path5"));
            Debug.Log("Resuming spawning...");
            fileLeftToSpawn = FilePerWave(); // Reset files to spawn for next wave
           
        }
        else if (fileLeftToSpawn <= 0)
        {
            Debug.Log("Wave complete! Starting next wave...");
            yield return new WaitForSeconds(timeBetweenWaves);
            fileLeftToSpawn = FilePerWave();
        }
    }
    }
}

// Helper method to check if a GameObject with a specific name exists
private bool GameObjectExists(string objectName)
{
    return GameObject.Find(objectName) != null;
}

    private int FilePerWave()
    {
        if(isSpawning ==true) {
            return Random.Range(2, 100); // Random number of files per wave
        } else {
        return Random.Range(0,0);
        }
         
    }

    private void SpawnFile()
{
    GameObject prefabToSpawn = filePrefabs[Random.Range(0, filePrefabs.Length)];
    spawnedFile = Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);

    // Assign a unique name to the spawned file
    spawnedFile.name = "path" + fileCounter;
    fileCounter++;
    

    spawnedFiles.Add(spawnedFile);
    FileMovement fileMovement = spawnedFile.GetComponent<FileMovement>();
      if (fileMovement != null)
        {
          
            actionButton.onClick.AddListener(fileMovement.OnEndButtonClicked);
            actionButton.onClick.AddListener(OnEndButtonClicked);
          
            Debug.LogWarning("ActionButton works for " + spawnedFile.name);
             
            
        }
    changeUI = true;
  
    
}
    public void OnEndButtonClicked()
{
   actionButton.interactable = false;
    StartCoroutine( checkingButton.OnCancelClickedIenumerator());

    fileMovement = GameObject.Find("path1").GetComponent<FileMovement>();
    if (fileMovement != null)
    {
        // Reset the checkButton color
        checkingButton.UpdateCheckButtonColor(new Color(0.7372549f, 0.7372549f, 0.7372549f));
        checkingButton.SetCanCheck(false); // Disable file checking
    } else{
        checkingButton.SetCanCheck(true); // Disable file checking
    }

    changeUI = false;
}

private IEnumerator ReEnableActionButton()
{
     yield return new WaitForSeconds(1f);
     actionButton.interactable = true;
}

public void OnAcceptFile()
{
    if(isAccepted == true) {
    
    }
}

public void StopWave()
{
    isSpawning = false;
    // Ensure the spawning coroutine is stopped
    if (spawningCoroutine != null)
    {
        StopCoroutine(spawningCoroutine);
    }
}

    

    
}
