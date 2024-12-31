using System.Collections;
using System.Collections.Generic;

using JetBrains.Annotations;
using UnityEngine;
using TMPro;


[System.Serializable]
public class GameData
{
    public long lastUpdated;
   public int totalGold;
   public Vector3 playerPosition;
    public SerializableDictionary<string, bool > goldCollected;

    public Vector3 cameraPosition;
    public string profileId;
    public int mrBlackDialogueState;
    public int mrWhiteDialogueState;
    public int mrGreenDialogueState;
    public int mrYellowDialogueState;
    public int mechanicDialogueState;
    public int Mechanic2DialogueState;
    public int malayNeighbourDialogueState;
    public int AgusDialogueState ;
    public int asepDialogueState ;
    public int tokoJusDialogueState ;
    public int datokDialogueState ;
    public int playerXP;
    public float money;
    public bool alreadyBoughtBalloon;
    public float moneyCanGenerate;
    public float kerjaCermatScore;
     public int satgasDataScore;
    public int energi;
    public float musicVolume;
    public float sfxVolume;

    public int hours;
    public int minutes;
    public int seconds;

    public int day;
 
    public bool talkedToParents;
    public bool udahAmbilUang;

    public int QuestLetterToYellow;
     public int PickingGoldQuest;
     public int BeriBerkasKeAsep;
     public int BeriTahuTemanKerja;

     public int questRewardStateSatgasData;
     public bool satgasDataDoneReward;

     public int questRewardStateKerjaCermat;
     public bool kerjaCermatDoneReward;
     public bool isPlayerThere;
    
     
    
    

   

    public GameData()
    {
        this.totalGold = 0  ;
        playerPosition = new Vector3(-125f,-0.9f,0f); 
        goldCollected = new SerializableDictionary<string, bool>();
        cameraPosition = new Vector3(-130f,3.5f,-10);
      
       mrBlackDialogueState = 1;
       mrWhiteDialogueState = 1;
       mrGreenDialogueState = 1;
       mrYellowDialogueState = 1;
        mechanicDialogueState = 1;
         malayNeighbourDialogueState = 1;
         AgusDialogueState = 1;
         asepDialogueState = 1;
         tokoJusDialogueState = 1;
         Mechanic2DialogueState =1;
         datokDialogueState = 1;

       playerXP = 0;
       money = 0;
       moneyCanGenerate =150;
       kerjaCermatScore = 0;
       satgasDataScore= 0;
       energi = 4;

       musicVolume = 0.3f;
       sfxVolume = 0.3f;

       day =1;
       hours =6;
       minutes =0;
       seconds =0;
       alreadyBoughtBalloon = false;
       talkedToParents = false;
        udahAmbilUang = false;
    
        QuestLetterToYellow =1;
        PickingGoldQuest =1;
        BeriBerkasKeAsep =1;
        BeriTahuTemanKerja =1;

        questRewardStateSatgasData =1;
        satgasDataDoneReward =false;

        questRewardStateKerjaCermat =1;
        kerjaCermatDoneReward = false;
        isPlayerThere = false;
       

       
    
    }
}
