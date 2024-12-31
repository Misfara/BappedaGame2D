    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame1 : MonoBehaviour,IDataPersistence
{
    [SerializeField] private GameObject triggerQue;
    [SerializeField] private GameObject startPanel;
    [SerializeField] public Image button;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject focusOnDialogue;
    [SerializeField] private Button playButton;
        [SerializeField] private GameObject TheGameItselfpanel;
    [SerializeField] private GameObject TheGameItself;
      [SerializeField] private Animator TheGameItselfAnimator;
    [SerializeField] private GameObject drawAndDiscard;
    [SerializeField] private GameObject roundEndPanel;
     [SerializeField] private GameObject card;
      [SerializeField] public TextMeshProUGUI score;
      [SerializeField] public float currentBestScore;
      [SerializeField] private DigitalClock digitalClock;
[SerializeField]Store store;
     EnergyToPlayMiniGame energyToPlayMiniGame;
     [SerializeField] public GameObject reward1;
         [SerializeField]public  GameObject reward2;
          [SerializeField] public GameObject reward3;
          [SerializeField] public GameObject reward4;
          
     public int questRewardStateKerjaCermat =1;
     public bool rewardDone = false;


    private bool playerCanClick;
    public bool canPlay;
    StopWatch stopWatch;

    Player player;

    public void Start()
    {
         stopWatch = FindAnyObjectByType<StopWatch>();
        player = GameObject.FindObjectOfType<Player>();
        energyToPlayMiniGame = FindAnyObjectByType<EnergyToPlayMiniGame>();
        score.text = currentBestScore.ToString();
          if(questRewardStateKerjaCermat ==1)
        {
            reward1.SetActive(true);
            reward2.SetActive(false);
            reward3.SetActive(false);
            reward4.SetActive(false);
        }

        if(questRewardStateKerjaCermat ==2)
        {
            reward1.SetActive(false);
            reward2.SetActive(true);
            reward3.SetActive(false);
            reward4.SetActive(false);
        }

        if(questRewardStateKerjaCermat==3){
            reward1.SetActive(false);
            reward2.SetActive(false);
            reward3.SetActive(true);
             reward4.SetActive(false);
        }

         if(questRewardStateKerjaCermat==4){
            reward1.SetActive(false);
            reward2.SetActive(false);
            reward3.SetActive(false);
            reward4.SetActive(true);
        }

        
     

        
    }

   
    public void OnTriggerEnter2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            triggerQue.SetActive(true);
            playerCanClick = true;
            Color tempColor = button.color;            
            tempColor.a = 1f; 
            button.color = tempColor;

        }
    }

    public void OnTriggerExit2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            triggerQue.SetActive(false);
            playerCanClick = false;
            Color tempColor = button.color;
            tempColor.a = 0.3f; 
            button.color = tempColor;
        }
    }

    public void OnClickedButton(){

       if(playerCanClick == true)
         { 
            startPanel.SetActive(true);
             player.canMove = false;
             backButton.SetActive(true);
             focusOnDialogue.SetActive(true);
         }
    }

    public void OnBackButtonClicked()
    {
        startPanel.SetActive(false);
        backButton.SetActive(false);
        focusOnDialogue.SetActive(false);
         player.canMove = true;
    }

    public void PlayGame()
    {
        if (playButton != null && energyToPlayMiniGame.currentEnergy >= 2)
        {
            StartCoroutine(OnPlayButtonPressed());
            energyToPlayMiniGame.currentEnergy = energyToPlayMiniGame.currentEnergy -2;
            AudioManager.Instance.musicSource.Stop();
        }
    }

    public IEnumerator OnPlayButtonPressed()
    {
        digitalClock.clockCanStart = false;
       TheGameItselfpanel.SetActive(true);
        yield return StartCoroutine(StartingAnimation());
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlayMusic("KerjaCermat");
       TheGameItself.SetActive(true);
       drawAndDiscard.SetActive(true);
       card.SetActive(true);
       yield return new WaitForSeconds(2f);
       TheGameItselfAnimator.SetBool("Starting",false);
       yield return new WaitForSeconds(2f);
       TheGameItselfpanel.SetActive(false);
    }

    public IEnumerator StartingAnimation()
{
    TheGameItselfAnimator.SetBool("Starting", true);

    // Wait until the "Starting" animation is done
    while (TheGameItselfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Starting")&&
           TheGameItselfAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
    {
        yield return null; // Wait for the next frame
    }

   
    
}   

    public IEnumerator OutingAnimation()
    {
        yield return new WaitForSeconds(0f);
        
    }


    public IEnumerator OnEndGameAnimation()
    {
         TheGameItselfpanel.SetActive(true);
        yield return StartCoroutine(StartingAnimation());
        yield return new WaitForSeconds(2f);
       TheGameItself.SetActive(false);
       drawAndDiscard.SetActive(false);
       roundEndPanel.SetActive(false);
       card.SetActive(false);
       yield return new WaitForSeconds(2f);
       TheGameItselfAnimator.SetBool("Starting",false);
       AudioManager.Instance.musicSource.Stop();
       yield return new WaitForSeconds(2f);
       TheGameItselfpanel.SetActive(false);
       
        TheGameItselfAnimator.SetBool("Done",true);
        
        AudioManager.Instance.PlayMusic("Theme");
        
    }

    public void OnEndGameButtonClicked()
    {
        StartCoroutine(OnEndGameAnimation());

    }

    public void CheckReward()
{
    // Debug to check the questRewardState and score
   
    
    // Check for questRewardState 1
    if (questRewardStateKerjaCermat == 1 && KerjaCermat.kinerjaTotal >= 20000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.3f;
        reward1.SetActive(true); // Activate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        questRewardStateKerjaCermat++; // Move to the next state
        Debug.Log("Reward 1 activated");
        if(KerjaCermat.kinerjaTotal >= 20000)
        {
           reward1.SetActive(false);
           reward2.SetActive(true);
        }
        return; // Exit after rewarding
        
    }

    // Check for questRewardState 2
    if (questRewardStateKerjaCermat== 2 && KerjaCermat.kinerjaTotal >= 35000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.5f;
        reward2.SetActive(true); // Activate the second reward
        reward1.SetActive(false); // Deactivate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        questRewardStateKerjaCermat++; // Move to the next state
        Debug.Log("Reward 2 activated");
        if(KerjaCermat.kinerjaTotal>= 35000)
        {
           reward1.SetActive(false);
           reward2.SetActive(false);
           reward3.SetActive(true);
        }
        return;
    }

    // Check for questRewardState 3
    if (questRewardStateKerjaCermat == 3 && KerjaCermat.kinerjaTotal >= 50000 && !rewardDone)
    {
        store.moneyCanGenerate *= 1.75f;
        questRewardStateKerjaCermat++;
        reward3.SetActive(true); // Activate the third reward
        reward2.SetActive(false); // Deactivate the second reward
        reward1.SetActive(false); // Deactivate the first reward
        store.moneyCanGenerateText.text = store.moneyCanGenerate.ToString();
        if(KerjaCermat.kinerjaTotal>= 50000)
        {
           reward1.SetActive(false);
           reward2.SetActive(false);
           reward3.SetActive(false);
           reward4.SetActive(true);
        }

       
        return;
    }

    if (questRewardStateKerjaCermat == 4 && KerjaCermat.kinerjaTotal >= 50000 && !rewardDone)
    {
       reward4.SetActive(true);
        reward3.SetActive(true); // Activate the third reward
        reward2.SetActive(false); // Deactivate the second reward
        reward1.SetActive(false); // Deactivate the first reward
        
        rewardDone = true; // Mark the rewards as done
        Debug.Log("Reward 3 activated");
    }
}


    public void SaveData(GameData data)
    {
        data.kerjaCermatScore = this.currentBestScore;
        data.questRewardStateKerjaCermat = this.questRewardStateKerjaCermat;
    data.kerjaCermatDoneReward = this.rewardDone;
        
    }

    public void LoadData(GameData data)
    {
        this.currentBestScore = data.kerjaCermatScore ;
          this.questRewardStateKerjaCermat = data.questRewardStateKerjaCermat;
        this.rewardDone = data.kerjaCermatDoneReward;
       
    }

    

    
}
