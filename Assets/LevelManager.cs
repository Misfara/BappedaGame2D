using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    public Transform path1;
    public Transform path2;
    public Transform path3;
    public Transform path4;
    public Transform path5;
    public Transform End;
    public Transform startPoint;

    [SerializeField] private Button StartButton;
    [SerializeField] private GameObject checkButton;
    [SerializeField] private GameObject confirmButton;
    [SerializeField] private Animator checkButtonAnimator;
    [SerializeField] TextMeshProUGUI skorText;

   
    public int totalScore;
    [SerializeField] public TextMeshProUGUI scoreInBeforeGame;

    private FileSpawner fileSpawner;
    private FileMovement fileMovement;
    CheckingButton checkingButton;

    public int levelOfPath = 1;

    public bool songEndingIsStarted =false;

    [Header("Requirements")]
    [SerializeField] public GameObject[] requirements;  // Array of requirement GameObjects

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        fileSpawner = FindObjectOfType<FileSpawner>();
        fileMovement = FindObjectOfType<FileMovement>();
        StartButton.onClick.AddListener(OnStartButtonClicked);
        StartButton.onClick.AddListener(fileSpawner.StartWave);
        checkingButton = FindAnyObjectByType<CheckingButton>();
         
     
      skorText.text = "0";
    }

    private void Update()
    {
        skorText.text = totalScore.ToString();
        if(checkingButton.elapsedTime <= 0 && songEndingIsStarted != true)
        {
            StartCoroutine(EndingMusic());
            songEndingIsStarted = true;
            
        }
         
    }

    public void OnStartButtonClicked()
    {
        checkButton.SetActive(true);
        fileSpawner.isSpawning=true;
        checkButtonAnimator.SetBool("OpeningCheck", true);
        AudioManager.Instance.musicSource.Stop();

        RandomizeRequirements();  // Randomize the requirements when start button is clicked
        StartCoroutine(StartingMusic());
       
    }

    private void RandomizeRequirements()
    {
        // Randomly select an index from the requirements array
        int randomIndex = Random.Range(0, requirements.Length);

        // Deactivate all requirements first
        foreach (var req in requirements)
        {
            req.SetActive(false);
        }

        // Activate the randomly selected requirement
        requirements[randomIndex].SetActive(true);
    }

     public void OnConfirmButtonClicked()
    {
        Debug.Log("Confirm button clicked");

        // Perform any other tasks needed after confirmation (like checking file, updating score, etc.)
        
        // Randomize requirements after confirmation
        RandomizeRequirements();  // Randomize again after confirmation
    }

    public IEnumerator StartingMusic()
    {
    yield return new WaitForSeconds(1f);
     AudioManager.Instance.PlayMusic("SatgasData");
    }

    public IEnumerator EndingMusic()
    { 
         AudioManager.Instance.musicSource.Stop();
        yield return new WaitForSeconds(1f);
       
        AudioManager.Instance.PlayMusic("MiniGameEnd");
    }

     public void DeactivateAllRequirements()
    {
        foreach (var req in requirements)
    {
        req.SetActive(false);  // Deactivate each requirement in the array
    }
    }

    
}

