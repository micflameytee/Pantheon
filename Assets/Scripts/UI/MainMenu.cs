using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject focusButton;

    void Start()
    {
        // Focus a button so you can select them on controller
        eventSystem.SetSelectedGameObject(focusButton);
    }
    
    public void LoadLobby()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("GameplayScene");
        // Utility.LoadLevel("Lobby");
    }

    public void QuitGame()
    {
        SFX.Instance.PlaySound("ui_select");
        Application.Quit();
    }
}
