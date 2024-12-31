using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;

    [Header("Menu Button")]
    [SerializeField] private Button backButton;

    [Header("Confirmation PopUp")]
    [SerializeField] private ConfirmationPopUpMenu confirmationPopUpMenu;
    private SaveSlot[] saveSlots;

    
    private bool isLoadingGame = false; 
    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {

        DisableMenuButton();
        if(isLoadingGame)
        {
            AudioManager.Instance.musicSource.Stop();
            DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        else if (saveSlot.hasData)
         {

            confirmationPopUpMenu.ActivateMenu(
                " Memulai ulang permainan ? ",
                // for yes
                ()=>{
                    AudioManager.Instance.musicSource.Stop();
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                // for no
                () => {
                    this.ActivateMenu(isLoadingGame );

                } 
            );
        }
        else {
                    DataPersistenceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistenceManager.instance.NewGame();
                    SaveGameAndLoadScene();
        }

    }

    private void SaveGameAndLoadScene()
    {
        AudioManager.Instance.musicSource.Stop();
        DataPersistenceManager.instance.SaveGame();

        SceneManager.LoadSceneAsync("Scene2");
    }

    public void OnDeleteClicked(SaveSlot saveSlot)
    {
        DisableMenuButton();
        AudioManager.Instance.PlaySFX("Denied");
        confirmationPopUpMenu.ActivateMenu(
            "Yakin untuk hapus ? " ,
            ()=> {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
        ActivateMenu(isLoadingGame);
            },
            ()=>
            {
                ActivateMenu(isLoadingGame);
            }
        );
        
    }

    public void DisableMenuButton()
    {
        foreach(SaveSlot saveSlot in saveSlots)
        {
            saveSlot.SetInteractable(false);
        }
        backButton.interactable = false;
    }

    public void OnBackClicked()
    {
        AudioManager.Instance.PlaySFX("Deny");
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
        mainMenu.title.SetActive(true);
        mainMenu.loadGameText.SetActive(false);
        mainMenu.newGameText.SetActive(false);
    }


    public void ActivateMenu(bool isLoadingGame)
    {

        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfilesGameData();

        backButton.interactable= true;

        foreach(SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if(profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            } else{
                saveSlot.SetInteractable(true);
            }
        }   
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    
}
