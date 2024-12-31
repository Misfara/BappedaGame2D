using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
   [Header("Profile")]
   [SerializeField] private string profileId = "";

   [Header("Content")]
   [SerializeField] private GameObject noDataContent;
   [SerializeField] private GameObject hasDataContent;
   [SerializeField] private TextMeshProUGUI nameText;
   [SerializeField] private TextMeshProUGUI goldCountText;

   [Header("Clear Data Button")]
   [SerializeField] private Button clearButton; 

   public bool hasData{ get; private set;} = false;

   private Button saveSlotButton;

   private void Awake()
   {
    saveSlotButton = this.GetComponent<Button>();
   }
   

   public void SetData(GameData data)
   {
    if(data== null)
    {
        hasData= false;
        noDataContent.SetActive(true);
        hasDataContent.SetActive(false);
        clearButton.gameObject.SetActive(false);
    } else{
        hasData = true;
        noDataContent.SetActive(false);
        hasDataContent.SetActive(true);
        clearButton.gameObject.SetActive(true);

        nameText.text = profileId.ToString();
    }
   }

   public string GetProfileId()
   {
    return this.profileId;
   }

   public void SetInteractable(bool interactable)
   {
    saveSlotButton.interactable = interactable;
    clearButton.interactable = interactable;
   } 
}
