using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatgasDataPlayButton : MonoBehaviour
{
    [SerializeField] Button playButton;
    EnergyToPlayMiniGame energyToPlayMiniGame;
    // Start is called before the first frame update
    void OnEnable()
    {
        energyToPlayMiniGame = FindAnyObjectByType<EnergyToPlayMiniGame>();
        if(energyToPlayMiniGame.currentEnergy >= 2 )
        {
            playButton.interactable = true;

        } else {
            playButton.interactable = false;
        }

    }

   
}
