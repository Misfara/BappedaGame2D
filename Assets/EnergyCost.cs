using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class EnergyCost : MonoBehaviour
{
     public static EnergyCost Instance; // Singleton instance
    [SerializeField] private TextMeshProUGUI energyText;
    public int energy =0;
    public int maxEnergy = 3;
    public bool playThisCard = false;
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Set singleton instance
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }
    void Start()
    {
       
        energy = 3;
        energyText.text = energy.ToString();
    }

    public void Update()
    {
        energyText.text = energy.ToString();
    }

    public void ResetEnergy()
    {
        energy = maxEnergy;
    }

    
}
