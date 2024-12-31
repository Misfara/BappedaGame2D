using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PickingGoldQuest : MonoBehaviour,IDataPersistence
{
    [SerializeField] private GameObject questDescription;
    [SerializeField] private Image questText;

    [SerializeField] private GameObject questNull;
    [SerializeField] private GameObject questStart;
    [SerializeField] private GameObject questDone;
    [SerializeField] private GameObject descOnProgress;
    [SerializeField] private GameObject descDone;

    public int questState = 1; 
    public int maxQuestState = 3;


    private void Start()
    {
        questDescription.SetActive(false);
           if (questText != null)
        {
            questText.color = new Color(1f, 1f, 1f, 0.3921569f);
        }
        OnEnteringLogQuest();
    }

   

    public void OnButtonClicked()
    {
        OnEnteringLogQuest();
        questDescription.SetActive(true);
       
        
        
    }

   

    public void OnIncreasingState()
    {
        questState ++;
        if(questState>maxQuestState)
        {
            questState = maxQuestState;
        }
        Debug.Log("Quest" + questState);
        OnEnteringLogQuest();
    }

    public void OnEnteringLogQuest()
    {
        if(questState == 1)
        {
            questNull.SetActive(true);
        }

        if(questState==2)
        {
            questStart.SetActive(true);
            descOnProgress.SetActive(true);
            
            questNull.SetActive(false);
            
        }

        if(questState ==3)
        {
            questDone.SetActive(true);
            descDone.SetActive(true);

            questStart.SetActive(false);
            questNull.SetActive(false);
            descOnProgress.SetActive(false);
        }
    }

     public void SaveData(GameData data)
    {
        data.PickingGoldQuest = this.questState;
    }

    public void LoadData(GameData data)
    {
        this.questState = data.PickingGoldQuest ;
    }
}
