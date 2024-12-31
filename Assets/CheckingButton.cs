using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CheckingButton : MonoBehaviour,IDataPersistence
{
    [Header("Requirements")]
        [SerializeField] public GameObject[] requirements;
        [SerializeField] Animator requirementsAnimator;
        [SerializeField] RequirementImage requirementImage;

    [Header("File")]
        

    [Header("CheckButton")]
        [SerializeField] public Button checkButton;
        [SerializeField] Animator checkAnimator;

    [Header("cancelButton")]
        [SerializeField]GameObject cancelButton;
        [SerializeField] Animator cancelAnimator;
     
    
    [Header("confirmButton")]
        [SerializeField]GameObject confirmButton;
        [SerializeField] Animator confirmAnimator;

    [Header("StopWatch")]
        [SerializeField] TextMeshProUGUI timerText;
        [SerializeField]public float elapsedTime = 120;
        private bool startCountdown = false;

    [Header("GameIsDone")]
        [SerializeField] Animator EndGameAnimator;
        [SerializeField] GameObject EndGamePanel;
        public GameObject loadingPanel;
        public GameObject loadingScreenTransition;
        [SerializeField] GameObject before;
        [SerializeField] GameObject HUD;
        [SerializeField] TextMeshProUGUI score;
        [SerializeField] LevelManager levelManager;
        [SerializeField] DigitalClock digitalClock;
        [SerializeField] GameObject reward1;
         [SerializeField] GameObject reward2;
          [SerializeField] GameObject reward3;
        
         [SerializeField] Transform mainCamera;
          
        public int currentBestScore = 0;

     FileMovement fileMovement;
     public bool canCheck = false;
     public bool forSpawner;
     FileSpawner fileSpawner;

     public bool canShowFile = false;
     EnergyToPlayMiniGame energyToPlayMiniGame;
     Store store;
     private int questRewardState =1;
     private bool rewardDone = false;
   

     private void Start()
     {
          
        canCheck = true;
        fileSpawner = FindAnyObjectByType<FileSpawner>();
        checkButton.interactable = false;
        energyToPlayMiniGame = FindAnyObjectByType<EnergyToPlayMiniGame>();
        store = FindAnyObjectByType<Store>();
        LevelManager.main.scoreInBeforeGame.text = currentBestScore.ToString();
        if(questRewardState ==1)
        {
            reward1.SetActive(true);
            reward2.SetActive(false);
            reward3.SetActive(false);
        }

        if(questRewardState ==2)
        {
            reward1.SetActive(false);
            reward2.SetActive(true);
            reward3.SetActive(false);
        }

        if(questRewardState ==3)
        {
            reward1.SetActive(false);
            reward2.SetActive(false);
            reward3.SetActive(true);
        }

        
     }

     public void Update()
{
    if(startCountdown == true)// Run the stopwatch regardless of the existence of path5
    StartStopwatch();

   

    // Check for path5 and handle fileSpawner and related logic
    GameObject path5 = GameObject.Find("path5");
    if (path5 != null && fileSpawner.changeUI == true)
    {
        fileMovement = GameObject.Find("path1").GetComponent<FileMovement>();

        if (fileMovement.hasReachedEnd == true)
        {
            checkButton.image.color = new Color(1f, 0.8223138f, 0.6273585f); // Normalize RGB to 0-1 range
            checkButton.interactable = true;
        }
        else
        {
            checkButton.image.color = new Color(0.7372549f, 0.7372549f, 0.7372549f); // Normalize RGB to 0-1 range
        }

        
    }
}

    public void OnButtonClicked()
    {   
        
        canShowFile = true;
         fileMovement = GameObject.Find("path1").GetComponent<FileMovement>();
        if(canCheck == true && fileMovement.hasReachedEnd == true  ) 
        {
             Color buttonColor = checkButton.image.color; 
            buttonColor.a = 1f;                  
            checkButton.image.color = buttonColor;  

              foreach (var req in requirements)
        {
            req.SetActive(false);
        }

        // Activate only the selected requirement
        foreach (var req in requirements)
        {
            if (req.activeSelf)  // Check if requirement is already activated (based on LevelManager's randomization)
            {
                req.SetActive(true);
                break;  // Only activate the first active requirement
            }
        }
            AudioManager.Instance.PlaySFX("MouseClick");

            requirementsAnimator.SetBool("Opening Requirement",true);
            
            canShowFile = true;
            cancelButton.SetActive(true);
            cancelAnimator.SetBool("OpeningCancel",true);

            confirmButton.SetActive(true);
            confirmAnimator.SetBool("OpeningConfirm",true);
            
            canCheck =false;
            checkAnimator.SetBool("OpeningCheck",false);
             fileSpawner.isAccepted = true;
            startCountdown = true; 
           
        } 


    }
    

    public IEnumerator OnCancelClickedIenumerator()
{
  
        Debug.Log("OnCancelClickedIenumerator started");
        
        requirementsAnimator.SetBool("Opening Requirement", false);
        
        cancelAnimator.SetBool("OpeningCancel",false);
        confirmAnimator.SetBool("OpeningConfirm",false);
        yield return new WaitForSeconds(1f);

        canShowFile = false;
        cancelAnimator.SetTrigger("Done");
        requirementsAnimator.SetTrigger("Done");
        confirmAnimator.SetTrigger("Done");
         checkAnimator.SetTrigger("Done");
        Debug.Log("Triggers set to 'Done'");
        checkAnimator.SetBool("OpeningCheck",true);
        yield return new WaitForSeconds(0.5f);
        

         foreach (var req in requirements)
            {
                req.SetActive(true);  // Activate the requirement that was set to true
            }
       
        cancelButton.SetActive(false);
        confirmButton.SetActive(false);
        

        Debug.Log("Objects deactivated, resetting canCheck to true");
       
       
        
   
}

    public void OnCancelClicked()
    {
    
        StartCoroutine(OnCancelClickedIenumerator());
        canCheck = true;
    }

    public void UpdateCheckButtonColor(Color color)
{
    checkButton.image.color = color;
}
public void SetCanCheck(bool value)
{
    canCheck = value;
}

public void StartStopwatch()
   {
          if(startCountdown == false)
         return;
    
     if(startCountdown == true) {
            if(elapsedTime >0)
        {
            elapsedTime -= Time.deltaTime;
        }
            else if(elapsedTime <= 0)
        {
             elapsedTime = 0;
             timerText.color = Color.red;
             OnStopwatchZero();
        }
       
            int minutes = Mathf.FloorToInt(elapsedTime/60);
            int seconds = Mathf.FloorToInt(elapsedTime%60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes,seconds);
     }
   }

   private void OnStopwatchZero()
   {
    StopAllCoroutines();
    StartCoroutine(OnCancelClickedIenumerator());

   score.text = LevelManager.main.totalScore.ToString();
    EndGamePanel.SetActive(true);
    EndGameAnimator.SetBool("GameDone",true);

    startCountdown = false;
   }

   public IEnumerator OnEndButtonClickedIenumerator()
   {
    fileSpawner.StopWave();
    AudioManager.Instance.PlaySFX("Deny");
        EndGameAnimator.SetBool("GameDone",false);
        yield return new WaitForSeconds(1f);
        EndGameAnimator.SetTrigger("GameDoneEnd");
        checkButton.gameObject.SetActive(false);
        EndGamePanel.SetActive(false);


        loadingPanel.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadingPanel.SetActive(false);
         AudioManager.Instance.musicSource.Stop();

  LevelManager.main.DeactivateAllRequirements();
       GameObject path1 =GameObject.Find("path1");
       if(path1 != null )
       Destroy(path1);

       GameObject path2 =GameObject.Find("path2");
       if(path2 != null )
       Destroy(path2);

       GameObject path3 =GameObject.Find("path3");
       if(path3 != null )
       Destroy(path3);

       GameObject path4 =GameObject.Find("path4");
       if(path4 != null )
       Destroy(path4);

       GameObject path5 =GameObject.Find("path5");
       if(path5 != null )

       Destroy(path5);

       fileSpawner.fileCounter =1;
       elapsedTime =120;
       energyToPlayMiniGame.currentEnergy = energyToPlayMiniGame.currentEnergy -1;
       canCheck = true;
     
     
         loadingScreenTransition.SetActive(true);
        GameObject player = GameObject.Find("Player");
        player.transform.position = before.transform.position;
        mainCamera.transform.position = new Vector3 (player.transform.position.x + 10, player.transform.position.y +4,-10);
         HUD.SetActive(true);
        yield return new WaitForSeconds(1f);
         
        AudioManager.Instance.PlayMusic("Theme");

        loadingScreenTransition.SetActive(false);
        fileSpawner.playerCanMove = true;
        checkButton.interactable = false;
        timerText.color = Color.black;
        LevelManager.main.totalScore =0;
       
       
        
   }

   public void OnEndButtonClicked()
   {
    StartCoroutine(OnEndButtonClickedIenumerator());
    CheckReward();
     
    
    
    if(LevelManager.main.totalScore > currentBestScore){
    LevelManager.main.scoreInBeforeGame.text = LevelManager.main.totalScore.ToString(); 
    currentBestScore = LevelManager.main.totalScore;
  
    }
    DataPersistenceManager.instance.SaveGame();
      digitalClock.hours = digitalClock.hours + 3;
   }

   public void CheckReward()
{
    // Debug to check the questRewardState and score
    Debug.Log("Current questRewardState: " + questRewardState);
    Debug.Log("Current score: " + LevelManager.main.totalScore);
    
    // Check for questRewardState 1
    if (questRewardState == 1 && LevelManager.main.totalScore >= 2000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.3f;
        reward1.SetActive(true); // Activate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        questRewardState++; // Move to the next state
        Debug.Log("Reward 1 activated");
        if(LevelManager.main.totalScore >= 2000)
        {
           reward1.SetActive(false);
           reward2.SetActive(true);
        }
        return; // Exit after rewarding
        
    }

    // Check for questRewardState 2
    if (questRewardState == 2 && LevelManager.main.totalScore >= 4000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.5f;
        reward2.SetActive(true); // Activate the second reward
        reward1.SetActive(false); // Deactivate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        questRewardState++; // Move to the next state
        Debug.Log("Reward 2 activated");
        if(LevelManager.main.totalScore >= 4000  &&questRewardState == 2)
        {
           reward1.SetActive(false);
           reward2.SetActive(false);
           reward3.SetActive(true);
        }
        return;
    }

    // Check for questRewardState 3
    if (questRewardState == 3 && LevelManager.main.totalScore >= 6000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.75f;
        reward3.SetActive(true); // Activate the third reward
        reward2.SetActive(false); // Deactivate the second reward
        reward1.SetActive(false); // Deactivate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        rewardDone = true; // Mark the rewards as done
        Debug.Log("Reward 3 activated");
    }
}

    public void SaveData(GameData data)
    {
        data.satgasDataScore = this.currentBestScore;
        data.questRewardStateSatgasData = this.questRewardState;
        data.satgasDataDoneReward = this.rewardDone;
        
    }

    public void LoadData(GameData data)
    {
        this.currentBestScore=data.satgasDataScore ; 
        this.questRewardState = data.questRewardStateSatgasData;
        this.rewardDone = data.satgasDataDoneReward;
    }
}
