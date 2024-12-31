using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stairs : MonoBehaviour
{
  [SerializeField]private GameObject sleepPanel;
   [SerializeField]private Animator sleepPanelAnimator;
  [SerializeField]private GameObject trigger;
   [SerializeField] public Image button;
  [SerializeField] DigitalClock digitalClock;
  [SerializeField] Store store;

   public GameObject loadingPanel;

   private bool canClick= false;
    public GameObject loadingScreenTransition;
    EnergyToPlayMiniGame energyToPlayMiniGame;

  public void Start()
  {
    energyToPlayMiniGame = FindAnyObjectByType<EnergyToPlayMiniGame>();
  }
    public void OnTriggerEnter2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            Color tempColor = button.color;
            tempColor.a = 1f; // Set alpha to 0
            button.color = tempColor;
            trigger.SetActive(true);
            canClick =true;
        }
    }

     public void OnTriggerExit2D(Collider2D player)
    {
        if(player.CompareTag("Player"))
        {
            Color tempColor = button.color;
            tempColor.a = 0.3f; // Set alpha to 0
            button.color = tempColor;
            trigger.SetActive(false);
            canClick =false;
        }
    }

    public void OnButtonClicked()
    {
        if(canClick){
        sleepPanel.SetActive(true);
        sleepPanelAnimator.SetBool("OpenSleepPanel",true);
        }
    }
    public IEnumerator OnCancelButtonClickedIEnumerator()
    {
        sleepPanelAnimator.SetBool("OpenSleepPanel",false);
         AudioManager.Instance.PlaySFX("Deny");
        yield return new WaitForSeconds(1f);
        sleepPanelAnimator.SetTrigger("Done");
        sleepPanel.SetActive(false);
    }
    public void OnCancelButtonClicked()
    {
        StartCoroutine(OnCancelButtonClickedIEnumerator());
    }

    public void OnSleepButtonClicked()
    {
        AudioManager.Instance.PlaySFX("Accept");
         StartCoroutine(sleepAnimation());
         DataPersistenceManager.instance.SaveGame();
        
      
    }

    public IEnumerator sleepAnimation()
    {
        sleepPanel.SetActive(false);
         loadingPanel.SetActive(true);
          yield return new WaitForSeconds(1f);
           loadingPanel.SetActive(false);
     
         loadingScreenTransition.SetActive(true);

        digitalClock.hours  = 6;
        digitalClock.minutes =0;
        digitalClock.day ++;
        store.udahAmbilUang = false;
        energyToPlayMiniGame.currentEnergy = 4;
        digitalClock.UpdateBackgroundColors();
    digitalClock.UpdatePanelAlpha();

          yield return new WaitForSeconds(1f);
          
           loadingScreenTransition.SetActive(false);
           
           
    }
}
