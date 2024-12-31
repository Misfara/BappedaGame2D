using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;

public class StopWatch : MonoBehaviour,IDataPersistence
{
     public static StopWatch Instance;
   [SerializeField] TextMeshProUGUI timerText;
   [SerializeField]public float elapsedTime = 10;

   [SerializeField] private Animator animator;
   [SerializeField] private GameObject RoundStartPanel;
   [SerializeField] private TextMeshProUGUI numberOfRound;
     [SerializeField] private GameObject roundEndPanel;
      [SerializeField] private TextMeshProUGUI roundEndKinerjaTotalText;
      [SerializeField] private Animator endGame;
       [SerializeField] private DigitalClock digitalClock;

   private bool startCountdown = false;
   private DrawCard drawCard;
   private WildSlotManager wildSlotManager;
   private KerjaCermat kerjaCermat;


      private int currentRound = 0;
      private bool newRound = false;
      private float currentRoundKinerja = 0;

      private bool lose = false;
      MiniGame1 miniGame1;
      [SerializeField]Store store;
      
    

    

   public void Update()
   {
    StartStopwatch();
   }

    public void Start()
    {
        drawCard= FindAnyObjectByType<DrawCard>();
         kerjaCermat= FindAnyObjectByType<KerjaCermat>();
        wildSlotManager = FindAnyObjectByType<WildSlotManager>();
        numberOfRound.text = "0";
        miniGame1 = FindAnyObjectByType <MiniGame1>();
         StartCoroutine(DelayedStartRoundButton(3f)); // 2f is the delay in seconds

         
    }

    // Coroutine to delay StartRoundButton
    private IEnumerator DelayedStartRoundButton(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartRoundButton();  // Call StartRoundButton after the delay
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

   public IEnumerator StartAnimation()
   {
       
        startCountdown = false;
        RoundStartPanel.SetActive(true);
        wildSlotManager.OnCancelButtonClicked();
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("StartRound");
    
        yield return new WaitForSeconds(4f);
        currentRound ++;
        numberOfRound.text = currentRound.ToString();
        drawCard.ResetAvailableSlots();
        

   }

   public IEnumerator StartRound()
   {
       
        yield return StartCoroutine(StartAnimation());
        yield return StartCoroutine(drawCard.RecycleCard());
        
      
        addingTime();
        drawCard.DrawingCard();
        yield return new WaitForSeconds(0.75f);
        RoundStartPanel.SetActive(false);
        startCountdown= true;
       

   }


   public void StartRoundButton()
   {
        if(lose == true)
        return;

        StartCoroutine(StartRound());
        AudioManager.Instance.PlaySFX("Accept");
        
   }

    public void addingTime()
   {
   
       currentRoundKinerja = KerjaCermat.GetKinerjaTotalThisRound();  

      
       float additionalTime = currentRoundKinerja / 1000;
       elapsedTime += additionalTime;

       Debug.Log($"Round {currentRound}: Added {additionalTime} seconds to timer based on kinerja {currentRoundKinerja}.");

      
       kerjaCermat.kinerjaText.text = KerjaCermat.kinerjaTotal.ToString();
       KerjaCermat.thisRoundAddedKinerja = 0;
    
   }

   public void OnStopwatchZero()
   {
        StopAllCoroutines();
        AudioManager.Instance.musicSource.Stop();
        startCountdown = false;
        roundEndPanel.SetActive(true);
           AudioManager.Instance.PlayMusic("MiniGameEnd");
        drawCard.StopAllCoroutines();
        roundEndKinerjaTotalText.text = KerjaCermat.kinerjaTotal.ToString();

        if(miniGame1.currentBestScore <KerjaCermat.kinerjaTotal )
        {
            miniGame1.score.text  =KerjaCermat.kinerjaTotal.ToString();
            miniGame1.currentBestScore = KerjaCermat.kinerjaTotal;
        }
        digitalClock.hours = digitalClock.hours + 5;
        endGame.SetTrigger("EndGame");
        digitalClock.clockCanStart = true;
        miniGame1.CheckReward();

    
   }
   
public void SaveData(GameData data)
{
    
}

public void LoadData(GameData data)
{
   
}



  

    
 
}
