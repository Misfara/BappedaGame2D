using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLog : MonoBehaviour
{   
   [SerializeField] private GameObject questLog;
   [SerializeField] private GameObject backButton;
   Animator animator;
   Player player;

    public void Start()
    {
        questLog.SetActive(false);
        backButton.SetActive(false);
        player = GameObject.FindObjectOfType<Player>();
        animator= GetComponent<Animator>();
     }
   public void OnIconButtonClicked()
   {
      AudioManager.Instance.PlaySFX("Pause");
    questLog.SetActive(true);
    player.canMove = false;
    backButton.SetActive(true);
    animator.SetBool("QuestOpen",true);
   } 

    public IEnumerator OnBackButtonClickedIenumerator()
   {
       AudioManager.Instance.PlaySFX("UnPaused");
      animator.SetBool("QuestOpen",false);
   yield return new WaitForSeconds(1f);

   animator.SetTrigger("Done");
    questLog.SetActive(false);
    player.canMove = true;
    backButton.SetActive(false);
   }

   public void OnBackButtonClicked()
   {
      StartCoroutine(OnBackButtonClickedIenumerator());
   }
}
