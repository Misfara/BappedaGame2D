using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotMenu saveSlotMenu;
    [Header("Menu Button")]
    [SerializeField] public GameObject title;
    [SerializeField] private Button newGameButton;
    [SerializeField] public GameObject newGameText;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] public GameObject loadGameText;

    [SerializeField] private Button ExitGameButton;

    private void Start()
    {
        DisableButtonWhenDeletingData();
        AudioManager.Instance.PlayMusic("MainMenu");
    }

    private void OnEnable()
    {
        
    }

    private void DisableButtonWhenDeletingData()
    {
        if(!DataPersistenceManager.instance.HasGameData())
        {
            continueButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked()
    {
        AudioManager.Instance.PlaySFX("MouseClick");
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMenu();
        title.SetActive(false);
        newGameText.SetActive(true);
    }    

    public void OnLoadGameClicked()
    {
        AudioManager.Instance.PlaySFX("MouseClick");
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMenu();
        title.SetActive(false);
        loadGameText.SetActive(true);
    }

    public void OnExitGameClicked()
    {
        Application.Quit();
    }
    public void OnContinueClicked()
    {
        AudioManager.Instance.PlaySFX("MouseClick");
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Scene2");
        AudioManager.Instance.musicSource.Stop();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        loadGameButton.interactable = false;
        ExitGameButton.interactable = false;
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonWhenDeletingData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
