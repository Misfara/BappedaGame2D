using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    private float moveSpeed = 4f;
 

    private Transform target;
    private bool isMovingToEnd = false;
    public bool hasReachedEnd = false; // To prevent overwriting target
    FileSpawner fileSpawner;
    bool isPath1 = false;
    private CheckingButton checkingButton;

    private void Start()
    {
       fileSpawner = FindObjectOfType<FileSpawner>();
     checkingButton = FindObjectOfType<CheckingButton>();
    
    }

    private void Update()
    {
        
           MoveToNextSlot();
    }

   private void FixedUpdate()
{
    if (target == null) return;

    Vector2 direction = (target.position - transform.position).normalized;
    rb.velocity = direction * moveSpeed;
   

    if (Vector2.Distance(transform.position, target.position) < 0.1f)
    {
        rb.velocity = Vector2.zero;

        if (isMovingToEnd)
        {
            hasReachedEnd = true;

            // Notify CheckingButton to enable checking
            if (checkingButton != null)
            {
                checkingButton.UpdateCheckButtonColor(new Color(1f, 0.8223138f, 0.6273585f)); // Button becomes active
                checkingButton.SetCanCheck(true); // Enable file checking
            }

            Destroy(gameObject);
        }
    }
}

    public void MoveToNextSlot()
    {
        
        if(this.gameObject.name == "path1" && hasReachedEnd== false)
        {
            target = LevelManager.main.path1;

            if(Vector2.Distance(transform.position, target.position) < 0.1f)
            {
            hasReachedEnd = true;
            }
        }

         if(this.gameObject.name == "path2")
        {
            target = LevelManager.main.path2;
        }

         if(this.gameObject.name == "path3")
        {
            target = LevelManager.main.path3;
        }

         if(this.gameObject.name == "path4")
        {
            target = LevelManager.main.path4;
        }

         if(this.gameObject.name == "path5")
        {
            target = LevelManager.main.path5;
        }

    }

    

    public void OnEndButtonClicked()
{
    if (this.gameObject.name == "path1" && hasReachedEnd == true)
    {
        target = LevelManager.main.End; // Set target to the final destination
        moveSpeed = 5f;
        isMovingToEnd = true; // Trigger the final movement
    }
    else if (this.gameObject.name == "path2")
    {
        target = LevelManager.main.path1; // Move to path1
        moveSpeed = 4f;
      
        this.gameObject.name = "path1"; // Update the name
    }
    else if (this.gameObject.name == "path3")
    {
        target = LevelManager.main.path2; // Move to path2
        moveSpeed = 4f;
       
        this.gameObject.name = "path2"; // Update the name
    }
    else if (this.gameObject.name == "path4")
    {
        target = LevelManager.main.path3; // Move to path3
        moveSpeed = 4f;
    
        this.gameObject.name = "path3"; // Update the name
    }
    else if (this.gameObject.name == "path5")
    {
        target = LevelManager.main.path4; // Move to path4
        moveSpeed = 4f;
     
        this.gameObject.name = "path4"; // Update the name
    }
}

           
           
        

       
    
    
}
