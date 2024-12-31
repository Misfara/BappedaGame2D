using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyToPlayMiniGame : MonoBehaviour,IDataPersistence
{
   [SerializeField] private GameObject[] energi;

   [SerializeField]public int currentEnergy = 4;

    public void Update()
    {
        for(int i =0; i< energi.Length; i++ )
        {
            if (energi[i] != null) // Check if the GameObject exists
            {
                energi[i].SetActive(i < currentEnergy); // Activate if within currentEnergy, else deactivate
            }
        }
    }

    public void SaveData(GameData data)
    {
        data.energi = this.currentEnergy;
    }

    public void LoadData(GameData data)
    {
        this.currentEnergy = data.energi;
    }
}
