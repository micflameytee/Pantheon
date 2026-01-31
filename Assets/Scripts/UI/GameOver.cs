using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerText;
    public GameObject focusButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(focusButton);
    }
    
    public void SetWinner(string playerName)
    {
        winnerText.text = "Player " + playerName + " wins!";
    }
    
    public void LoadLobby()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.UnloadLevel("GameplayScene");
        Utility.LoadLevel("GameplayScene");
    }
    
    public void LoadMainMenu()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.UnloadLevel("GameplayScene");
        Utility.LoadLevel("MainMenu");
    }
}
