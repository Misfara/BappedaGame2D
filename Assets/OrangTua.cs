using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class OrangTua: MonoBehaviour,IDataPersistence
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private GameObject DialogueBox;
    [SerializeField] private TextMeshProUGUI nama;
     [SerializeField] private Image portrait;
      [SerializeField] private SpriteRenderer imageSprite;
   
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject focusOnDialogue;
    [SerializeField] private GameObject infoUi;
    
    
    [SerializeField] private GameObject DialoguePanel;
    
    [SerializeField] public Image button;

    private bool playerInRange;
    public bool dialogueStarted;

    private bool canInitiateDialogue;
  
     
     [SerializeField] private DialogueObject dialogueObject;
    
     [SerializeField] private TextMeshProUGUI xpUI;
      [SerializeField] private BoxCollider2D limit;

     private ResponseHandler responseHandler;
     private bool talkedToParents =false;


    private DialogueWriterEffect dialogueWriterEffect;
    private int orangTuaDialogueState = 1;
    private int maxDialogueState = 4;

    private DialogueResponse dialogueResponse;
    private Player player;



    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
        dialogueStarted = false;
        canInitiateDialogue = false;
        
          
    }

    public void Start()
    {
       
        dialogueText.text = "";
        dialogueWriterEffect = GetComponent<DialogueWriterEffect>();
        DataPersistenceManager.instance.LoadGame();
        responseHandler = GetComponent<ResponseHandler>();
        dialogueResponse = GetComponent<DialogueResponse>();
        player= FindObjectOfType<Player>();
        IsColliderOn();
     
          UpdateXPText();
    }


    private void Update()
    {
        if (playerInRange ) 
        {
            visualCue.SetActive(true);
            canInitiateDialogue = true;
        }
        else
        {
            visualCue.SetActive(false);
            canInitiateDialogue = false;
        }
    }

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {

        this.dialogueObject = dialogueObject;
    }

    public void OnButtonClicked()
    {
    
        if (playerInRange && !dialogueStarted && canInitiateDialogue)
        {
             if (orangTuaDialogueState<= 1 ){
           EnterDialogue(dialogueObject);
           dialogueStarted= true;
           player.canMove= false;
             }

             

        }
        else
        {
            visualCue.SetActive(false);
        }

        if(dialogueStarted == true )
        {
            canInitiateDialogue = false;
        }
        else{
            canInitiateDialogue = true;
            dialogueStarted = false;
              player.canMove= true;
        }
    }

    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.CompareTag("Player"))
        {
            playerInRange = true;
            Color tempColor = button.color;
            tempColor.a = 1f; // Set alpha to fully visible
            button.color = tempColor;
        }
    }

    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.CompareTag("Player"))
        {
            playerInRange = false;
            Color tempColor = button.color;
            tempColor.a = 0.3f; // Set alpha to semi-transparent
            button.color = tempColor;
        }
    }

    public void EnterDialogue(DialogueObject dialogueObject)
    {
        AudioManager.Instance.PlaySFX("TalkToNPC");
        
          foreach (DialogueResponse responseEvents in GetComponents<DialogueResponse>())
        {
            if(responseEvents.DialogueObject == dialogueObject)
            {
                AddResponseEvents(responseEvents.Events);
                break;
            }
        }
        focusOnDialogue.SetActive(true);
        nama.text = "Orang Tua";
        portrait.sprite = imageSprite.sprite;
        infoUi.SetActive(false);
        StartCoroutine(StepThroughDialog(dialogueObject));
        visualCue.SetActive(false);
        DialogueBox.SetActive(true);
         dialogueStarted = true;
         player.canMove= false;
         talkedToParents = true;
    }
    
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        responseHandler.AddResponseEvents(responseEvents);
    }

    

    private IEnumerator StepThroughDialog(DialogueObject dialogueObject)
    {

        for( int i = 0 ; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
             yield return dialogueWriterEffect.Run(dialogue,dialogueText);

             if(i == dialogueObject.Dialogue.Length -1 && dialogueObject.HasResponses) break; 
             
              yield return new WaitUntil(() =>
        {
            // Check if mouse click is over a UI element
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
            {
                // Optionally, check if the click was within DialogueBox bounds
                RectTransform dialoguePanelRect = DialoguePanel.GetComponent<RectTransform>();
                Vector2 localMousePosition = dialoguePanelRect.InverseTransformPoint(Input.mousePosition);

                if (dialoguePanelRect.rect.Contains(localMousePosition))
                {
                    return true;
                }
            }

             if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Check if the touch was within DialogueBox bounds
                    RectTransform dialoguePanelRect = DialoguePanel.GetComponent<RectTransform>();
                    Vector2 localTouchPosition = dialoguePanelRect.InverseTransformPoint(touch.position);
                    if (dialoguePanelRect.rect.Contains(localTouchPosition))
                    {
                        return true;
                    }
                }
            }
            return false;
        });
        }
        if(dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
         else
         {
            ExitingDialogue();
         }

        
   
    }

    public void ExitingDialogue()
    {
        DialogueBox.SetActive(false);
        dialogueText.text = "";
        dialogueStarted = false;
        DataPersistenceManager.instance.SaveGame();
        player.canMove= true;
        focusOnDialogue.SetActive(false);
        infoUi.SetActive(true);
        
    }

    
    public void IncreasingDialogueState()
    {
       orangTuaDialogueState++;
        if(orangTuaDialogueState > maxDialogueState)
        {
          orangTuaDialogueState= maxDialogueState;
        }
    }

 

    public void OnQuestDone()
      {
       
        player.playerXP += 500;
        UpdateXPText();  // Update the XP UI to show new value
         DataPersistenceManager.instance.SaveGame();
      } 

      private void UpdateXPText()
{
    if (xpUI != null)
    {
        xpUI.text = player.playerXP.ToString();
    }
}

  public void IsColliderOn()
{
    if( talkedToParents == true){
    limit.enabled = false;
    }
}

public void SaveData(GameData data)
{
    data.talkedToParents = this.talkedToParents;
}

public void LoadData(GameData data)
{
    this.talkedToParents = data.talkedToParents;
}
      
}