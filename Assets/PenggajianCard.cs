using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PenggajianCard : MonoBehaviour, ICard
{
     private static int kinerjaTotal = 0;
     private EnergyCost energyCost;
    [SerializeField] private int thisCardCost;
    [SerializeField] private Button continueButton;

     [SerializeField] private Button resetEffect;
  
    public int Cost => thisCardCost;
    public bool effect = false;
    public Animator animator;

    private DrawCard drawCard;
    private KerjaCermat kerjaCermat;
    private TargetTime targetTime;
    private StopWatch stopwatch;

    public void Start()
    {
        stopwatch = FindAnyObjectByType<StopWatch>();
        energyCost = FindAnyObjectByType<EnergyCost>();
         targetTime = FindAnyObjectByType<TargetTime>();
        kerjaCermat = FindAnyObjectByType<KerjaCermat>();
        drawCard = FindAnyObjectByType<DrawCard>();
        animator = GetComponent<Animator>();
         if (continueButton != null)
        {
            continueButton.onClick.AddListener(OnContinueButtonPressed);
             continueButton.onClick.AddListener(DiscardingCard);
        }
        else
        {
            Debug.LogError("Continue Button is not assigned in the Inspector!");
        }

        if (resetEffect != null)
        {
            resetEffect.onClick.AddListener(OnResetButtonPressed);
        }
        else
        {
            Debug.LogError("Continue Button is not assigned in the Inspector!");
        }
    }

    public bool CanPlay(int currentEnergy)
    {
        return currentEnergy >= thisCardCost;
    }

    public void Play()
    {    EnergyCost.Instance.energy -= thisCardCost;
        Debug.Log("TahunAnggaranCard played.");
      effect = true;
    }

    private void OnContinueButtonPressed()
    {
        if( effect == false)
        return;
        if(effect == true){   
           
             stopwatch.elapsedTime += 10;
            targetTime.AddTime(0,20);
           
            effect = false; 
        }
        
        
    }

    private void OnResetButtonPressed()
    {
        effect = false;
    }

public void DiscardingCard()
    {
         if (!this.gameObject.activeSelf)
    {
        Debug.Log("GameObject already inactive. Returning.");
        return;
    }
        drawCard.discardPile.Add(this.gameObject);
        drawCard.discardPileText.text =  drawCard.discardPile.Count.ToString();
         
          StartCoroutine(StartAnimation());
       
  
    }

    public IEnumerator StartAnimation()
{
    // Start the fade-out animation
    animator.SetBool("FadingOut",true);

    // Get the animation's length using AnimatorStateInfo
    AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);
    
    // Wait until the animation finishes playing
    while (animationState.normalizedTime < 1.0f || !animationState.IsName("FadingOut"))
    {
        animationState = animator.GetCurrentAnimatorStateInfo(0); // Continuously check the state
        yield return null; // Wait for the next frame
    }
   

    // Deactivate the GameObject after the animation is done
    this.gameObject.SetActive(false);
    
}
    
}