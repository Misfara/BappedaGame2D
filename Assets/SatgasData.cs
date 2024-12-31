  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatgasData : MonoBehaviour
{
    [SerializeField] private GameObject triggerQue;
    [SerializeField] private GameObject startPanel;
    [SerializeField] public Image button;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject focusOnDialogue;
 

    private bool playerCanClick;
    public bool canPlay;
    StopWatch stopWatch;

    Player player;

    public void Start()
    {
         stopWatch = FindAnyObjectByType<StopWatch>();
        player = GameObject.FindObjectOfType<Player>();
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
}