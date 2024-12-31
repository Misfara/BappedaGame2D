/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine.Events;

public class NPCDialogue : MonoBehaviour, IDataPersistence
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    [SerializeField] private GameObject DialogueBox;
   
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    
    [SerializeField] private GameObject DialoguePanel;
    
    [SerializeField] public Image button;

    private bool playerInRange;
    public bool dialogueStarted;

    private bool canInitiateDialogue;
  
     [SerializeField]private DialogueObject Dialogue2;
     [SerializeField] private DialogueObject dialogueObject;

     private ResponseHandler responseHandler;


    private DialogueWriterEffect dialogueWriterEffect;
    private int dialogueState = 1;
    private int maxDialogueState = 2;

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

        if(dialogueState > 1)
        {
            this.dialogueObject = Dialogue2;
        }
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
        if(TryGetComponent(out DialogueResponse responseEvent))
        {
            AddResponseEvents(responseEvent.Events);
        }
        if (playerInRange && !dialogueStarted && canInitiateDialogue)
        {
             if ( dialogueState <= 1 ){
           EnterDialogue(dialogueObject);
           dialogueStarted= true;
           player.canMove= false;
             }

             if(dialogueState >= 2)
             EnterDialogue(Dialogue2);
              dialogueStarted= true;
              player.canMove= false;
          
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
       
        StartCoroutine(StepThroughDialog(dialogueObject));
        DialogueBox.SetActive(true);
         dialogueStarted = true;
         player.canMove= false;
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

    private void ExitingDialogue()
    {
        DialogueBox.SetActive(false);
        dialogueText.text = "";
        dialogueStarted = false;
        DataPersistenceManager.instance.SaveGame();
        player.canMove= true;

    }

     public void LoadData (GameData data)
    {
        this.dialogueState = data.dialogueState;
    }

    public void SaveData( GameData data)
    {
       
       data.dialogueState = this.dialogueState;
    }

    public void IncreasingDialogueState()
    {
        dialogueState++;
        if(dialogueState > maxDialogueState)
        {
            dialogueState = maxDialogueState;
        }
    }
    
}*/