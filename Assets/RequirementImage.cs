using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;

public class RequirementImage : MonoBehaviour
{
     public static LevelManager main;
    [SerializeField] Button terimaButton;
    [SerializeField] Button tolakButton;
    [SerializeField] Button confirmButton;
    [SerializeField] ImageFile imageFile;
    

    public bool isAccepted;
    CheckingButton checkingButton;
    int startHour = 7; 
    int endHour = 16;
    private bool isChecked = false; 

    private bool nameIsGood = false;
    private bool nameLengthIsGood = false;
    private bool sizeIsGood = false;
     private bool timeIsGood = false;
      private bool extensionIsGood = false;
      private int sizeLimit ;

      FileSpawner fileSpawner;
      LevelManager levelManager;
      

    public void OnEnable()
    {
    
      

        terimaButton.onClick.AddListener(OnTerimaButtonClicked);
        tolakButton.onClick.AddListener(OnTolakButtonClicked);
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        
        
        checkingButton = FindObjectOfType<CheckingButton>();
        
        fileSpawner = FindAnyObjectByType<FileSpawner>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void OnDisable()
    {
       
        confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
    }

    public void OnTerimaButtonClicked()
    {
        isAccepted = true;
        fileSpawner.actionButton.interactable = true;
        SetButtonOpacity(terimaButton, 1f); // Set opacity of Terima button to 0.5
        SetButtonOpacity(tolakButton, 0.5f); // Set opacity of Tolak button back to 1
    }

    public void OnTolakButtonClicked()
    {
        isAccepted = false;
        fileSpawner.actionButton.interactable = true;
        SetButtonOpacity(terimaButton, 0.5f); // Set opacity of Terima button to 0.5
        SetButtonOpacity(tolakButton, 1f); // Set opacity of Tolak button back to 1
    }
    private void SetButtonOpacity(Button button, float opacity)
{
    Color color = button.image.color;
    color.a = opacity;
    button.image.color = color; // Change the alpha value to adjust opacity
}

    public void OnConfirmButtonClicked()
    {
      CheckToAcceptOrDeny();
      AudioManager.Instance.PlaySFX("MouseClick");
      fileSpawner.actionButton.interactable = false;
      checkingButton.elapsedTime += 3;
        confirmButton.onClick.AddListener(LevelManager.main.OnConfirmButtonClicked);
    }

   public void Update()
{
    

    if (checkingButton.canCheck == false)
    {
        CheckingFile();
    }
}

    public void CheckingFile()
    {
    
        imageFile = GameObject.Find("path1").GetComponent<ImageFile>();
        
        //1 
        if(imageFile.fileName.text == imageFile.modifiedFileName)
        {
            nameIsGood = true;
          
           
        } else {
            nameIsGood = false;
           
        }  

    
        
        if(levelManager.requirements[0].activeSelf)
        {

            //1
            if(imageFile.randomExtension != ".jpg")
            {
                extensionIsGood = true;
            } else {
                extensionIsGood = false;
            }

            
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = false;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = false;
        }

            //2

             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = false;
            }

            //3
             if(imageFile.randomExtension == ".exe")
            {
                extensionIsGood = false;
            }

            //4

             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
            {
                nameLengthIsGood = true;
          
           
            }       else {
            nameLengthIsGood = false;
           
            }

            //5
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }

        }

        if(levelManager.requirements[1].activeSelf)
        {
            //1
            if(imageFile.randomExtension != ".exe")
            {
                extensionIsGood = true;
            } else {
                extensionIsGood = true;
            }

            
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = false;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = false;
        }

            //2

             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                 else 
            {
                timeIsGood = false;
            }

            //3

             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
             {
            nameLengthIsGood = true;
          
           
            } else {
            nameLengthIsGood = false;
           
            }

            //4
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }
        }


         if(levelManager.requirements[2].activeSelf)
        {
            //1
            if(imageFile.randomExtension != ".exe")
            {
                extensionIsGood = true;
            }   
                else 
            {
                extensionIsGood = false ;
            }

            
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = false;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = false;
        }

            //2
            if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = true;
            }

            //3

             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
            {
            nameLengthIsGood = true;
          
           
            } else {
            nameLengthIsGood = false;
           
            }

            //4
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }

        }

         if(levelManager.requirements[3].activeSelf)
        {
            //1
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi",".exe" ,".zip",".rar"}.Contains(imageFile.randomExtension))
            {
                extensionIsGood = false;
            }   
                else 
            {
                extensionIsGood = true;
            }

            
      

            //2

             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
            {
            nameLengthIsGood = true;
          
           
            } else {
            nameLengthIsGood = false;
           
            }

            //3
          
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = true;
            }

            //4 
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
             {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }

        }

        if(levelManager.requirements[4].activeSelf)
        {
            //1
             if(imageFile.originalFileName.Length <= 15 && imageFile.originalFileName.Length>= 10 )
            {
            nameLengthIsGood = true;
        
            } else {
            nameLengthIsGood = false;
           
            }

            
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = false;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = false;
        }

            //2
            if(imageFile.randomExtension != ".exe")
            {
                extensionIsGood = true;
            } else {
                extensionIsGood = false;
            }

            //3
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = true;
            }

            //4

            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }
        }

        if(levelManager.requirements[5].activeSelf)
        {
            //1
             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
            {
            nameLengthIsGood = true;
        
            } else {
            nameLengthIsGood = false;
           
            }

       

            //2
             if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi",".exe" 
                            ,".jpg", ".jpeg", ".png",".rar",".zip"}.Contains(imageFile.randomExtension))
            {
                extensionIsGood = false;
            }   
                else 
            {
                extensionIsGood = true;
            }

            //3
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = false;
            }

            //4
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }
        }

        if(levelManager.requirements[6].activeSelf)
        {
            //1
             if(imageFile.originalFileName.Length <= 15 && imageFile.originalFileName.Length>= 8 )
            {
            nameLengthIsGood = true;
        
            } else {
            nameLengthIsGood = false;
           
            }

            
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = false;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = false;
        }

            //2
            if(imageFile.randomExtension != ".exe")
            {
                extensionIsGood = true;
            } else {
                extensionIsGood = true;
            }

            //3
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = true;
            }

            //4
            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = false;
            }
        } 
            else
            {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
             }
        }



         if(levelManager.requirements[7].activeSelf)
        {
            //1
             if(imageFile.originalFileName.Length <= 15 && imageFile.originalFileName.Length>= 8 )
            {
            nameLengthIsGood = true;
        
            } else {
            nameLengthIsGood = true;
           
            }

            //2
            if(imageFile.randomExtension != ".exe")
            {
                extensionIsGood = true;
            } else {
                extensionIsGood = true;
            }

            //3
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = true;
            }

            //4

            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  = true;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = true;
            }
        }

        
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = true;
        } else  
        {
            extensionIsGood = true;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood = true;
        }  else 
        {
            extensionIsGood = true;
        }
        }

      if(levelManager.requirements[8].activeSelf)
        {
            //1
             if(imageFile.originalFileName.Length <= 12 && imageFile.originalFileName.Length>= 10 )
            {
            nameLengthIsGood = true;
        
            } else {
            nameLengthIsGood = false;
           
            }


            //3
             if(imageFile.hour>= startHour && imageFile.hour <= endHour)
            {
                timeIsGood = true;
            }   
                else 
            {
                timeIsGood = false;
            }

            //4

            if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(imageFile.randomExtension))
        {
            if(imageFile.randomFileSize <= 1000)
            {
                sizeIsGood = true;
            }
                else
            {
                sizeIsGood  =false;
            }
        } 
            else
        {
            if(imageFile.randomFileSize <= 10)
            {
                sizeIsGood = true;
            } else 
            {
                sizeIsGood = false;
            }
        }

        
        if(imageFile.randomExtension != ".zip")
        {
            extensionIsGood = false;
        } else  
        {
            extensionIsGood = true;
        }



        if(imageFile.randomExtension != ".rar")
        {
            extensionIsGood =false;
        }  else 
        {
            extensionIsGood = true;
        }
        }


       


      


    }   

    public void CheckToAcceptOrDeny()
{
    bool allChecksPassed = nameIsGood && sizeIsGood && extensionIsGood && timeIsGood && nameLengthIsGood;

    if (isAccepted)
    {
        LevelManager.main.totalScore += allChecksPassed ? 500 : -700;
    }
    else
    {
        LevelManager.main.totalScore += allChecksPassed ? -700 : 500;
    }
}
}
