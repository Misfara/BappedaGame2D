using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectCoinsQuestStep : QuestStep
{
    private int coinsCollected =0;
    private int coinsToComplete = 2;
    TestingBlock testingBlock;

    public void Start()
    {
        testingBlock = FindAnyObjectByType<TestingBlock>();
    }



    private void CoinCollected()
    {
        if(coinsCollected <= coinsToComplete)
        {
            coinsCollected ++; 
        }

        if(coinsCollected> coinsToComplete)
        {
            FinishQuestStep();
        }
    }
    
}
