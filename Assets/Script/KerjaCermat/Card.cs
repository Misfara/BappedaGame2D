using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;

    private KerjaCermat kerjaCermat;

    private void Start()
    {
        kerjaCermat = FindObjectOfType<KerjaCermat>();
    }

    

}
