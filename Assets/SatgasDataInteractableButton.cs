using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatgasDataInteractableButton : MonoBehaviour
{
    [SerializeField] EnergyToPlayMiniGame energyToPlayMiniGame;
    [SerializeField] Button thisButton;

    
    void OnEnable()
    {
        if(energyToPlayMiniGame.currentEnergy >=1)
        {
            thisButton.interactable = true;
        }  else 
        {
            thisButton.interactable = false;
        }
    }
}
