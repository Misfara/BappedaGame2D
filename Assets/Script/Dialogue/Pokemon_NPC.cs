using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pokemon_NPC : MonoBehaviour
{
   [SerializeField] private Color defaultColor = Color.white;
   [SerializeField] private Color charmanderColor = Color.red;
   [SerializeField] private Color bulbasaurColor = Color.green;
   [SerializeField] private Color squiertleColor = Color.blue;

   private SpriteRenderer spriteRenderer;

   private void Start()
   {
    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
   }

   private void Update()
   {
    //string pokemonName = ((Ink.Runtime.StringValue)DialogueManager.GetInstance()
    //.GetVariableState("pokemon_name")).value;

    /*switch(pokemonName)
    {
        case "":
        spriteRenderer.color = defaultColor;
        break;

        case "Charmander" :
        spriteRenderer.color = charmanderColor;
        break;

        case "Bulbasaur" :
        spriteRenderer.color = bulbasaurColor;
        break;

        case "Squirtle" :
        spriteRenderer.color = squiertleColor;
        break;

        default :
            Debug.Log("Pokemon name not handled by switch statement " +pokemonName);
            break;
    }*/
   }


}
