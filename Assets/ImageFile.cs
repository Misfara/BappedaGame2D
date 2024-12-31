using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageFile : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI fileName;
    [SerializeField] private TextMeshProUGUI fileDescription;
    [SerializeField] GameObject file;
    [SerializeField] Animator fileAnimator;

    [SerializeField] Button checkButton;
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;

    CheckingButton checkingButton;
    FileMovement fileMovement;

    public string modifiedFileName; 
    public string originalFileName; 
    public string randomExtension;
    public int randomFileSize;
    public int randomFileTime;
    public   int hour, minute , fileSize;

    bool fileIsOpened = false;

    void Start()
    {
    
     originalFileName = GenerateRandomFileName();
     randomExtension = GenerateRandomFileExtension();
    string fullFileName = originalFileName + randomExtension;

   
    string randomFileSize = GenerateRandomFileSize(randomExtension);
    string randomFileTime = GenerateRandomFileTime();

    // Generate a modified version of the file name
    modifiedFileName = GenerateModifiedFileName(originalFileName)+randomExtension ;

    // Set file name and description
    fileName.text = fullFileName;
        fileDescription.text = "File ini memiliki nama " + modifiedFileName + " ukuran sebesar "
                                + GenerateRandomFileSize(randomExtension) + " yang didapat pada waktu " + GenerateRandomFileTime(); 

        checkButton = GameObject.Find("CheckingButton")?.GetComponent<Button>();
        checkButton.onClick.AddListener(TheFileStats);
        fileMovement = GetComponent<FileMovement>();
        checkingButton = FindAnyObjectByType<CheckingButton>();
    }

    private void Update ()
    {
        if(fileIsOpened == true){
        cancelButton = GameObject.Find("TutupButton")?.GetComponent<Button>();
        cancelButton.onClick.AddListener(OnCancelClicked);

        confirmButton = GameObject.Find("Selesai Button")?.GetComponent<Button>();
        confirmButton.onClick.AddListener(OnCancelClicked);

        if(checkingButton.elapsedTime == 0){
        OnCancelClicked();
        }
        } else {
            return;
        }
    }

    public string GenerateRandomFileName()
    {
        System.Random random = new System.Random();
    int length = random.Next(8, 15); // 13 is exclusive, so it picks 10, 11, or 12

    System.Text.StringBuilder result = new System.Text.StringBuilder();

        for (int i = 0; i < length; i++)
        {
            // Generate a random lowercase letter
            char letter = (char)random.Next('a', 'z' + 1);

            // 10% chance to capitalize the letter
            if (random.Next(0, 100) < 10) // 10% probability
            {
                letter = char.ToUpper(letter);
            }

            result.Append(letter);
        }

        return result.ToString();
    }

     public string GenerateRandomFileExtension()
{
    // List of possible file extensions
    string[] extensions = { ".jpg", ".jpeg", ".png", ".bmp", ".webp",".mp4",".3gp",".wmv",".mov",".avi",".rar",".zip" 
    ,".doc",".pdf",".ppt",".exe"};

    // Randomly select one extension
    System.Random random = new System.Random();
    int randomIndex = random.Next(0, extensions.Length);

    return extensions[randomIndex];

    
}

public string GenerateRandomFileSize(string fileExtension)
{
    System.Random random = new System.Random();
    int fileSize = 0; // Default file size

    if (new string[] { ".mp4", ".3gp", ".wmv", ".mov", ".avi" }.Contains(fileExtension))
    {
        int chance = random.Next(0, 100); // Chance for weighted probability for video files

        if (chance < 85) // 75% chance for size between 100 MB - 1000 MB
        {
            fileSize = random.Next(1, 1001); // Range: 100-1000 MB
        }
        else // 25% chance for size bigger than 1000 MB
        {
            fileSize = random.Next(1001, 5001); // Range: 1001-5000 MB (can adjust the upper limit as needed)
        }
    }
    else
    {
        int chance = random.Next(0, 100); // Chance for weighted probability for other files

        if (chance < 75) // 75% chance for size between 1 MB - 10 MB
        {
            fileSize = random.Next(1, 11); // Range: 1-10 MB
        }
        else // 25% chance for size bigger than 10 MB
        {
            fileSize = random.Next(11, 2001); // Range: 11-50 MB (can adjust the upper limit as needed)
        }
    }

    return fileSize + " MB";
}
    public string GenerateRandomFileTime()
    {
        System.Random random = new System.Random();
        int chance = random.Next(0, 100); // Chance for weighted probability

      

        if (chance < 80) // 65% chance for time between 07:30–16:00
        {
            hour = random.Next(7, 17); // Range: 7–16 (inclusive)
            minute = random.Next(0, 60); // Range: 0–59
        }
        else // 35% chance for any time (00:00–23:59)
        {
            hour = random.Next(0, 24); // Range: 0–23
            minute = random.Next(0, 60); // Range: 0–59
        }

        // Format time as "HH:mm" in military time
        return hour.ToString("D2") + ":" + minute.ToString("D2");
    }

    public string GenerateModifiedFileName(string originalFileName)
{
    System.Random random = new System.Random();
    System.Text.StringBuilder result = new System.Text.StringBuilder();

    for (int i = 0; i < originalFileName.Length; i++)
    {
        char letter = originalFileName[i];

        // 30% chance to replace the letter with a random lowercase letter
        if (random.Next(0, 100) < 3) // 30% probability
        {
            letter = (char)random.Next('a', 'z' + 1);
        }

        result.Append(letter);
    }

    return result.ToString();
}

   public void TheFileStats()
{
    // Check if this GameObject is named "path1" and the file has reached its end
    if (this.gameObject.name == "path1" && fileMovement.hasReachedEnd)
    {
        // Wait until only one GameObject named "path1" exists
        int path1Count = CountObjectsWithName("path1");
        if (path1Count == 1)
        {
            // Activate the file and play the animation
            file.SetActive(true);
            fileAnimator.SetBool("FileOpening", true);
            fileIsOpened = true;
        }
    }
}

// Helper method to count GameObjects with a specific name
private int CountObjectsWithName(string objectName)
{
    int count = 0;

    // Find all GameObjects in the scene
    GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

    // Count objects that match the specified name
    foreach (GameObject obj in allObjects)
    {
        if (obj.name == objectName)
        {
            count++;
        }
    }
    return count;
}

    public IEnumerator OnCancelClickedIenumerator()
    {
        fileIsOpened= false;
        fileAnimator.SetBool("FileOpening", false);
        yield return new WaitForSeconds(1f);

        fileAnimator.SetTrigger("Done");
        yield return new WaitForSeconds(0.5f);
        
        file.SetActive(false);
    }

    public void OnCancelClicked()
    {
        StartCoroutine(OnCancelClickedIenumerator());
         
    }


}
