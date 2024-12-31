using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour,IDataPersistence
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] Animator pausePanelAnimator;
    [SerializeField] Button cancelButton;
    DigitalClock digitalClock;

    public Slider _musicSlider, _sfxSlider;


    public void Start()
    {
      
        digitalClock = FindAnyObjectByType<DigitalClock>();
         AudioManager.Instance.MusicVolume(_musicSlider.value);
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
        _musicSlider.value = 0.5f;
        _sfxSlider.value = 0.5f;
    }

    public void OnButtonClicked()
    {
        digitalClock.clockCanStart = false;
         cancelButton.interactable = true;
        pausePanel.SetActive(true);
        pausePanelAnimator.SetBool("PauseClicked",true);
        AudioManager.Instance.PlaySFX("Pause");
        AudioManager.Instance.musicSource.Pause();
    }

    public IEnumerator OnCancelButtonClickedTime()
    {

        pausePanelAnimator.SetBool("PauseClicked",false);
        digitalClock.clockCanStart = true;
        
        AudioManager.Instance.PlaySFX("UnPaused");
        yield return new WaitForSeconds(1f);
        pausePanelAnimator.SetTrigger("Done");
        pausePanel.SetActive(false);
        cancelButton.interactable = false;
        
        AudioManager.Instance.PlayMusic("Theme");
    }

    public void OnCancelButtonClicked()
    {
        StartCoroutine(OnCancelButtonClickedTime());
    }

    public void OnMenuButtonClicked()
    {
        DataPersistenceManager.instance.SaveGame();
        digitalClock.clockCanStart = false;
        SceneManager.LoadScene("MainMenu");
         AudioManager.Instance.PlaySFX("Deny");
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }

    public void SaveData(GameData data)
    {
        data.musicVolume = _musicSlider.value;
        data.sfxVolume = _sfxSlider.value;
    }
     public void LoadData(GameData data)
    {
        this._musicSlider.value = data.musicVolume ;
        this._sfxSlider.value = data.sfxVolume;
    }
}
