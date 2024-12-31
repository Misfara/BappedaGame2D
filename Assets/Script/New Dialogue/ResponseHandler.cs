using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResponseHandler : MonoBehaviour
{
   [SerializeField] private RectTransform responseBox;
   [SerializeField] private RectTransform responseButtonTemplate;
   [SerializeField] private RectTransform responseContainer;
   [SerializeField]public MrWhiteDialogue mrWhiteDialogue;
   [SerializeField]public MrBlackDialogue mrBlackDialogue;
   [SerializeField]public MrGreenDialogue mrGreenDialogue;
   [SerializeField]public MrYellowDialogue mrYellowDialogue;
   [SerializeField]public Mechanic mechanic;
   [SerializeField]public Fisherman fisherman;
   [SerializeField]public MalayNeighbours malayNeighbours;
   [SerializeField]public Agus agus;
   [SerializeField]public Asep asep;
   [SerializeField]public TokoJus tokoJus;
   [SerializeField]public Mechanic2 mechanic2;
   [SerializeField]public Datok datok;
    [SerializeField]public Child child;
    [SerializeField]public Jajat jajat;
    [SerializeField]public NPCSatgasData nPCSatgasData;
    [SerializeField]public NPCKerjaCermat nPCKerjaCermat;
    [SerializeField]public OrangTua orangTua;

   private ResponseEvent[] responseEvents;

   List<GameObject> tempResponseButton = new List<GameObject>();

   private void Start()
   {
     mrWhiteDialogue = GetComponent<MrWhiteDialogue>();
     mrBlackDialogue = GetComponent<MrBlackDialogue>();
      mrGreenDialogue = GetComponent<MrGreenDialogue>();
       mrYellowDialogue = GetComponent<MrYellowDialogue>();
   }


   public void AddResponseEvents(ResponseEvent[] responseEvents)
   {
      this.responseEvents = responseEvents;
   }

   public void ShowResponses(Response[] responses)
   {
  

     for(int i= 0; i < responses.Length;i++)
     {
         Response response = responses[i];
         int responseIndex = i;

        GameObject responseButton = Instantiate(responseButtonTemplate.gameObject,responseContainer);
        responseButton.SetActive(true);
        responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
        responseButton.GetComponent<Button>().onClick.AddListener(()=> OnPickedResponse(response,responseIndex));
         tempResponseButton.Add(responseButton);
      
     }
 
     responseBox.gameObject.SetActive(true);
   }

   private void OnPickedResponse(Response response, int responseIndex)
   {
      responseBox.gameObject.SetActive(false);

      foreach (GameObject button in tempResponseButton)
      {
         Destroy(button);
      }
      tempResponseButton.Clear();

      if(responseEvents != null && responseIndex < responseEvents.Length )
      {
       if (responseEvents[responseIndex].OnPickedResponse != null)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        if (responseEvents[responseIndex].AdditionalEffect != null)
        {
            responseEvents[responseIndex].AdditionalEffect?.Invoke();
        }
       
      }

      responseEvents = null;

       if(mrBlackDialogue != null){
           if(response.DialogueObject   ){
                mrBlackDialogue.EnterDialogue(response.DialogueObject);
               }  
               else {
                    mrBlackDialogue.ExitingDialogue();
               }
       }

      if(mrWhiteDialogue != null){

          if(response.DialogueObject ){
                mrWhiteDialogue.EnterDialogue(response.DialogueObject);
              }else {
                 mrWhiteDialogue.ExitingDialogue();
            }
      }

      if(mrGreenDialogue!= null )
         if(response.DialogueObject  ){
                mrGreenDialogue.EnterDialogue(response.DialogueObject);
          }else {
          mrGreenDialogue.ExitingDialogue();
       }
       
      if(mrYellowDialogue != null){
          if(response.DialogueObject  ){
               mrYellowDialogue.EnterDialogue(response.DialogueObject);
            }
             else {
                mrYellowDialogue.ExitingDialogue();
             }
      }

      if(mechanic!=null)
      {
         if(response.DialogueObject)
         {
                mechanic.EnterDialogue(response.DialogueObject);
         }else {
                mechanic.ExitingDialogue();
             }
      }

       if(fisherman!=null)
      {
         if(response.DialogueObject)
         {
                fisherman.EnterDialogue(response.DialogueObject);
         }else {
               fisherman.ExitingDialogue();
             }
      }

       if(malayNeighbours!=null)
      {
         if(response.DialogueObject)
         {
               malayNeighbours.EnterDialogue(response.DialogueObject);
         }else {
               malayNeighbours.ExitingDialogue();
             }
      }

       if(agus!=null)
      {
         if(response.DialogueObject)
         {
              agus.EnterDialogue(response.DialogueObject);
         }else {
               agus.ExitingDialogue();
             }
      }

       if(asep!=null)
      {
         if(response.DialogueObject)
         {
              asep.EnterDialogue(response.DialogueObject);
         }else {
               asep.ExitingDialogue();
             }
      }
      if(tokoJus!=null)
      {
         if(response.DialogueObject)
         {
              tokoJus.EnterDialogue(response.DialogueObject);
         }else {
               tokoJus.ExitingDialogue();
             }
      }

       if(mechanic2!=null)
      {
         if(response.DialogueObject)
         {
              mechanic2.EnterDialogue(response.DialogueObject);
         }else {
               mechanic2.ExitingDialogue();
             }
      }

      if(datok!=null)
      {
         if(response.DialogueObject)
         {
              datok.EnterDialogue(response.DialogueObject);
         }else {
              datok.ExitingDialogue();
             }
      }

      if(child!=null)
      {
         if(response.DialogueObject)
         {
              child.EnterDialogue(response.DialogueObject);
         }else {
              child.ExitingDialogue();
             }
      }

       if(jajat!=null)
      {
         if(response.DialogueObject)
         {
              jajat.EnterDialogue(response.DialogueObject);
         }else {
              jajat.ExitingDialogue();
             }
      }

       if(nPCSatgasData!=null)
      {
         if(response.DialogueObject)
         {
              nPCSatgasData.EnterDialogue(response.DialogueObject);
         }else {
              nPCSatgasData.ExitingDialogue();
             }
      }

       if(nPCKerjaCermat!=null)
      {
         if(response.DialogueObject)
         {
             nPCKerjaCermat.EnterDialogue(response.DialogueObject);
         }else {
              nPCKerjaCermat.ExitingDialogue();
             }
      }

       if(orangTua!=null)
      {
         if(response.DialogueObject)
         {
             orangTua.EnterDialogue(response.DialogueObject);
         }else {
              orangTua.ExitingDialogue();
             }
      }
   }

   
}
