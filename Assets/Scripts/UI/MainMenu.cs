using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject focusButton;
    
    public AudioMixer audioMixer;

    void Start()
    {
        // Focus a button so you can select them on controller
        eventSystem.SetSelectedGameObject(focusButton);

        // Set master volume 
        float volume = PlayerPrefs.GetFloat("MasterVolume", 100);
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume / 100) * 20f);
    }
    
    public void LoadLobby()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("CustomRules");
        // Utility.LoadLevel("Lobby");
    }
    
    public void LoadSettings()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("SettingsMenu");
    }

    public void QuitGame()
    {
        SFX.Instance.PlaySound("ui_select");
        Application.Quit();
    }
}
