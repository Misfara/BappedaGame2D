using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStart : MonoBehaviour,IDataPersistence
{
    private bool playerIsThere = false;

    public void Start()
    {
        if (playerIsThere==true)
        {
            AudioManager.Instance.PlayMusic("Theme");
        }
    }
   public void OnTriggerEnter2D(Collider2D player)
   {
        if(player.CompareTag("Player") && playerIsThere == false)
        {
            AudioManager.Instance.PlayMusic("Theme");
            playerIsThere= true;
        }
   }

   public void SaveData(GameData data)
   {
        data.isPlayerThere = this.playerIsThere;
   }

   public void LoadData(GameData data)
   {
        this.playerIsThere = data.isPlayerThere;
   }
}
