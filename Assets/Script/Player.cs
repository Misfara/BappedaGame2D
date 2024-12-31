using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Player : MonoBehaviour,IDataPersistence
{
    [SerializeField]float moveSpeed = 2;
      [SerializeField]GameObject dialogueBox ;
       [SerializeField]GameObject playerSpawn;
     
    SpriteRenderer spriteRenderer;
    
 
    Animator animator;

    FileSpawner fileSpawner;

    public bool canMove = false;

    public int playerXP =0;
    private bool audioIsDone = true;

    [SerializeField] private float audioCooldown = 0.3f; // Delay duration in seconds
private float lastAudioTime = 0; // Tracks the last time the sound was played
 
    public VectorValue startingPosition;
    // Start is called before the first frame update

    
    void Start()
{
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    canMove = true;
     
    // Check if there is saved data
    
    fileSpawner = GameObject.FindAnyObjectByType<FileSpawner>();
}

    // Update is called once per frame
    void Update()
    {

        if (fileSpawner.playerCanMove == false) {
        return;
        }
       
       if(dialogueBox.activeSelf == true)
       {
        return;
       }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return; // Do nothing if the click is over a UI element
        }

        Walking();
         
    }

    public void Walking()
    {

    if(canMove == false)
    return;
 if(dialogueBox.activeSelf == true)
       {
        return;
       }
        
        
         if(Input.GetMouseButton(0))
         {
            UnityEngine.Vector3 mousePos=Input.mousePosition;
            float screenMid = Screen.width / 2;
            animator.SetBool("Run",true);
            
              if (Time.time - lastAudioTime >= audioCooldown)
        {
            AudioManager.Instance.PlaySFX("Run");
            lastAudioTime = Time.time;
        }

            if( mousePos.x >screenMid)
            {
            
               UnityEngine.Vector2 direction = UnityEngine.Vector2.right;

                 transform.Translate(direction*Time.deltaTime * moveSpeed);
                 spriteRenderer.flipX = false ;
              

            } else
            {
                UnityEngine.Vector2 direction =  UnityEngine.Vector2.left;

                 transform.Translate(direction*Time.deltaTime * moveSpeed);
                  spriteRenderer.flipX = enabled ;
                  
            }
         } else
         {
            animator.SetBool("Run",false);
         }
    }

    public void LoadData (GameData data)
    {
        this.transform.position = data.playerPosition;
        this.playerXP = data.playerXP;
    }

    public void SaveData( GameData data)
    {
       
       data.playerPosition = this.transform.position;
       data.playerXP = this.playerXP;
    }

 

    
}
