/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Net;
using Unity.VisualScripting.Dependencies.Sqlite;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed =0.04f;

    [Header("Load Global File")]
    [SerializeField] private TextAsset loadGlobalScript;
     private GameData gameData;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [SerializeField] private TextMeshProUGUI xpDisplay;
 
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI [] choicesText;

    [Header("Audio")]
    [SerializeField] private DialogueAudioInfoSO defaultAudioInfo;
    [SerializeField] private DialogueAudioInfoSO[] audioInfos;
    [SerializeField] private bool makePredictable;

    private DialogueAudioInfoSO currentAudioInfo;
    private Dictionary<string, DialogueAudioInfoSO> audioInfoDictionary;
    private AudioSource audioSource;
    private Story currentStory;

    public bool dialogueIsPlaying {get;private set;}
    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;
     
    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";
    private const string GAVE_XP_TAG = "gaveXp";
    private const string QUEST_ID_TAG = "questId";
    private const string SAVING_PROGRESS = "save";

      bool xpAlreadyGained = false;
      int currentxp =0;
      private Dictionary<string, bool> xpGainedPerQuest = new Dictionary<string, bool>();
      private string currentQuestId = ""; // Track the current quest ID


    private DialogueVariable dialogueVariable;

    private InkExternalFunction inkExternalFunction;
    private int totalXp = 0;

    
    private void Awake()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        
        if( instance != null)
        {
            Debug.Log("Found More than one dialogue Manager");
        } 

        instance = this;
         gameData = new GameData(); 
        inkExternalFunction = new InkExternalFunction(); 
       
        currentAudioInfo = defaultAudioInfo;

    
         dialogueVariable = new DialogueVariable(loadGlobalScript); // Pass the instance
  
    
    }


    private void Start()
    {
          
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        layoutAnimator = dialoguePanel.GetComponent<Animator>();
    
        
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index ++;
        }
        InitializeAudioInfoDictionary();
    }
    
    private void InitializeAudioInfoDictionary()
    {
        audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
        audioInfoDictionary.Add(defaultAudioInfo.id , defaultAudioInfo);

        foreach (DialogueAudioInfoSO audioInfo in audioInfos)
        {
            audioInfoDictionary.Add(audioInfo.id , audioInfo);
        }
    }

    private void SetCurrentAudioInfo(string id)
    {
        DialogueAudioInfoSO audioInfo = null;
        audioInfoDictionary.TryGetValue(id,out audioInfo);
        if(audioInfo != null)
        {
            this.currentAudioInfo = audioInfo;
        } else{
            Debug.Log("Fialed to find audio");
        }
    }

    private void Update()
    {
        if(!dialogueIsPlaying)
        {
            return;
        }

       if (canContinueToNextLine &&     Input.GetKeyDown(KeyCode.Mouse0) && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
  
    }

  

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJson , Animator emoteAnimator )
    {
        currentStory = new Story(inkJson.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariable.StartListening(currentStory);
        inkExternalFunction.Bind(currentStory, emoteAnimator);
       
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("left");

       ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {

        yield return new WaitForSeconds(0.2f);

        dialogueVariable.StopListening(currentStory);
        inkExternalFunction.UnBind(currentStory);
       

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        SetCurrentAudioInfo (defaultAudioInfo.id);
        SaveProgress();
    }

    private void ContinueStory()
    {
         if(currentStory.canContinue)
        {
            if(displayLineCoroutine != null )
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();

            if(nextLine.Equals("") && !currentStory.canContinue)

            {
                StartCoroutine(ExitDialogueMode());
            } 
            else {
            HandleTags(currentStory.currentTags);
            displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
            
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        HideChoices();
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        canContinueToNextLine = false;
        continueIcon.SetActive(false);

        bool isAddingRichText = false;
      

        foreach(char letter in line.ToCharArray())
        {
             
            if(letter == '<'|| isAddingRichText)
        {
            isAddingRichText = true;
        
            if(letter == '>')
            {
                isAddingRichText = false;
            }
        } else{
           PlayDialogueSound(dialogueText.maxVisibleCharacters,dialogueText.text[dialogueText.maxVisibleCharacters]);
            dialogueText.maxVisibleCharacters ++;
          
            yield return new WaitForSeconds(typingSpeed);
        }

        }

        DisplayChoices();
         continueIcon.SetActive(true);
        canContinueToNextLine = true; 
    }

   private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
{
    AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
    int frequencyLevel = currentAudioInfo.frequencyLevel;
    float minPitch = currentAudioInfo.minPitch;
    float maxPitch = currentAudioInfo.maxPitch;
    bool stopAudioSource = currentAudioInfo.stopAudioSource;
    float volume = currentAudioInfo.volume; 

    if (currentDisplayedCharacterCount % frequencyLevel == 0)
    {
        if (stopAudioSource == true)
        {
            audioSource.Stop();
        }

        AudioClip soundClip = null;

        if (makePredictable)
        {
            int hashCode = currentCharacter.GetHashCode();
            int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
            soundClip = dialogueTypingSoundClips[predictableIndex];

            int minPitchInt = (int)(minPitch * 100);
            int maxPitchInt = (int)(maxPitch * 100);
            int pitchRangeInt = maxPitchInt - minPitchInt;

            if (pitchRangeInt != 0)
            {
                int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                float predictablePitch = predictablePitchInt / 100f;
                audioSource.pitch = predictablePitch;
            }
            else
            {
                audioSource.pitch = minPitch;
            }
        }
        else
        {
            int randomIndex = Random.Range(0, dialogueTypingSoundClips.Length);
            soundClip = dialogueTypingSoundClips[randomIndex];
            audioSource.pitch = Random.Range(minPitch, maxPitch);
        }

       
        audioSource.volume = volume;

        audioSource.PlayOneShot(soundClip);
    }
}

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
{
    foreach (string tag in currentTags)
    {
        string[] splitTag = tag.Split(':');

        // Check if the tag has a key-value pair (contains ':')
        if (splitTag.Length == 1)
        {
            // Single part tag, no key-value pair
            string tagKey = splitTag[0].Trim();

            switch (tagKey)
            {
                case SAVING_PROGRESS:
                    DataPersistenceManager.instance.SaveGame();
                    SaveProgress();
                    Debug.Log("Saving");
                    break;

                default:
                    Debug.LogWarning("Unknown single tag: " + tagKey);
                    break;
            }
        }
        else if (splitTag.Length == 2)
        {
            // Handle key-value tags (contains ':')
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;

                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;

                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;

                case AUDIO_TAG:
                    SetCurrentAudioInfo(tagValue);
                    break;

                case GAVE_XP_TAG:
                    if (int.TryParse(tagValue, out int xpGained))
                    {
                        ApplyXp(currentQuestId, xpGained);
                    }
                    break;

                case QUEST_ID_TAG:
                    currentQuestId = tagValue;
                    Debug.Log("Current Quest ID: " + currentQuestId);
                    break;

                default:
                    Debug.LogWarning("Unknown key-value tag: " + tagKey);
                    break;
            }
        }
        else
        {
            // This shouldn't happen, but in case the tag is formatted incorrectly
            Debug.LogError("TAG IS WRONG IDK");
        }
    }
}

    public void SaveProgress()
{
    // Serialize the current Ink story state to JSON
     if (currentStory != null)
    {
        string inkStateJson = currentStory.state.ToJson();
        
        // Save it in gameData (DataPersistenceManager's instance is used to save game)
        gameData.inkStoryState = inkStateJson;
    }
    
    // Save game progress through DataPersistenceManager
    DataPersistenceManager.instance.SaveGame();
    
    Debug.Log("Ink story state saved on application exit.");
}

public void LoadInkStoryState()
{
    if (!string.IsNullOrEmpty(gameData.inkStoryState) && currentStory != null)
    {
        // Restore the Ink story state from the saved JSON into the current story instance
        currentStory.state.LoadJson(gameData.inkStoryState);
        Debug.Log("Ink story state restored.");
    }
    else
    {
        Debug.LogWarning("No saved state found or currentStory is null.");
    }
}

  private void ApplyXp(string questId, int xp)
{
    if (!xpGainedPerQuest.ContainsKey(questId) || !xpGainedPerQuest[questId])
    {
        Debug.Log("Gained XP for " + questId + ": " + xp);
        xpGainedPerQuest[questId] = true; // Mark XP as gained for this quest
        totalXp += xp;
    }
    else
    {
        Debug.Log("XP already gained for " + questId);
    }
    xpDisplay.text = "XP = " + totalXp;
}

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if( currentChoices.Count > choices.Length)
        {
            Debug.LogError("Nah mate"+ currentChoices.Count);
        }

        int index =0 ;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index ; i<choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }


    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoices(int choiceIndex)
    {
        if(canContinueToNextLine){
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariable.variables.TryGetValue(variableName, out variableValue);

        if(variableValue ==null)
        {
            Debug.LogWarning("Ink Variable was found be null " + variableName);
        }
        return variableValue;
    }

    public void OnApplicationQuit()
    {
        SaveProgress();
    }
    
}
*/

