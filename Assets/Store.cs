using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour,IDataPersistence
{
    [SerializeField] public TextMeshProUGUI moneyText;
     [SerializeField] private GameObject trigger; 
    [SerializeField] public TextMeshProUGUI moneyCanGenerateText;
     [SerializeField] private GameObject storePanel;
    [SerializeField] public Image button;

     [SerializeField] private Animator storeAnimator;
     [SerializeField]TextMeshProUGUI hariCount;

      [SerializeField] private TextMeshProUGUI apakahSudahDiAmbil;

     DigitalClock digitalClock;
     public bool udahAmbilUang;
    

    public float moneyCanGenerate = 150;
    public float totalMoney ;

    public bool canCollectMoney = true;

    private bool canClick;

    public void OnTriggerEnter2D(Collider2D player)
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

     public void OnTriggerExit2D(Collider2D player)
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

    public void OnEnable()
    {
        moneyText.text = totalMoney.ToString();
        digitalClock = FindObjectOfType<DigitalClock>();
        moneyCanGenerateText.text = moneyCanGenerate.ToString();
         hariCount.text = digitalClock.day.ToString();
         
    }

  
    public void OnButtonClicked()
    {   
        if(canClick == true)
        {
        storePanel.SetActive(true);
        storeAnimator.SetBool("OpeningStore", true);
     
        }

        if(canCollectMoney)
        {
            apakahSudahDiAmbil.text = " BELUM DIAMBIL";
            apakahSudahDiAmbil.color = new Color(0.2f, 0.8f, 0.2f, 1f);
          
        } else {
            apakahSudahDiAmbil.text = " SUDAH DIAMBIL";
            apakahSudahDiAmbil.color = Color.red;
        }
        
    }

    public IEnumerator OnConfirmButtonClickedIEnumerator()
    {
          AudioManager.Instance.PlaySFX("Money");
        storeAnimator.SetBool("OpeningStore", false);
        yield return new WaitForSeconds(1f);
        storeAnimator.SetTrigger("Done");
        storePanel.SetActive(false);
    }

    public void OnConfirmButtonClicked()
    {
        StartCoroutine(OnConfirmButtonClickedIEnumerator());
        if(canCollectMoney)
        {
            moneyText.text = (totalMoney + moneyCanGenerate).ToString();
            totalMoney = totalMoney + moneyCanGenerate;
            canCollectMoney = false;
            udahAmbilUang = true;
            
        }
    }

    public void LoadData(GameData data)
    {
       this.totalMoney = data.money;
       moneyText.text = totalMoney.ToString();
       
       this.moneyCanGenerate = data.moneyCanGenerate;
       moneyCanGenerateText.text = moneyCanGenerate.ToString();

       this.udahAmbilUang = data.udahAmbilUang;
    }

    public void SaveData(GameData data)
    {
         data.money = this.totalMoney;
         data.moneyCanGenerate = this.moneyCanGenerate;
        data.udahAmbilUang = this.udahAmbilUang;
    }
}
