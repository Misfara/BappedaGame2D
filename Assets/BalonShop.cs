using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BalonShop : MonoBehaviour,IDataPersistence
{
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject stokHabis;
    [SerializeField] GameObject stokAda;
    [SerializeField] Button cancel;
      [SerializeField] Button beli;
      [SerializeField] public Image button;
    [SerializeField] DigitalClock digitalClock;
     [SerializeField] Store store;
     [SerializeField] GameObject trigger;

    private bool canClick = false;

    private bool alreadyBoughtBalloon= false;

     public void OnTriggerEnter2D (Collider2D player)
     {
        if(player.CompareTag("Player"))
        {
              canClick = true;
            Color tempColor = button.color;
            tempColor.a = 1f; // Set alpha to 0
            button.color = tempColor;
            trigger.SetActive(true);
        }
     }

       public void OnTriggerExit2D (Collider2D player)
     {
        if(player.CompareTag("Player"))
        {
            Color tempColor = button.color;
            tempColor.a = 0.3f; // Set alpha to 0
            button.color = tempColor;
            trigger.SetActive(false);
            canClick = false;
        }
     }
  


  

    public void OnShopClicked()
    {
        if(canClick == true){
            AudioManager.Instance.PlaySFX("MouseClick");
        shopPanel.SetActive(true);
        if(digitalClock.day ==1)
        {
            stokHabis.SetActive(true);
            cancel.gameObject.SetActive(true);
        }
         else if(digitalClock.day >=2 && alreadyBoughtBalloon == false)
        {
            stokAda.SetActive(true);
            stokHabis.SetActive(false);
            beli.gameObject.SetActive(true);
            cancel.gameObject.SetActive(true);
        } else {
            stokAda.SetActive(false);
            stokHabis.SetActive(true);
        }

        if(store.totalMoney < 150)
        {
           
        
            beli.interactable = false;
          
        } else {
            beli.interactable = true;
            
        }
        }
        
    }

    public void OnBeliButtonClicked()
    
    {
        if(store.totalMoney >= 150){
            store.totalMoney = store.totalMoney -150;
            store.moneyText.text = store.totalMoney.ToString();
            stokAda.SetActive(false);
            stokHabis.SetActive(true);
            alreadyBoughtBalloon = true;
             AudioManager.Instance.PlaySFX("Accept");
        } 
    }

    public void OnCancelButtonClicked()
    {
         AudioManager.Instance.PlaySFX("Deny");
        shopPanel.SetActive(false);
    }

    public void SaveData(GameData data)
    {
        data.alreadyBoughtBalloon = this.alreadyBoughtBalloon;
    }

      public void LoadData(GameData data)
    {
        this.alreadyBoughtBalloon = data.alreadyBoughtBalloon;
    }
}
