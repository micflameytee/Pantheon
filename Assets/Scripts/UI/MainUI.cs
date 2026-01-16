using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject lobbyMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        // Enable main menu, disable all other UI
        mainMenu.SetActive(true);
        lobbyMenu.SetActive(false);
    }

    public void EnterLobby()
    {
        mainMenu.SetActive(false);
        lobbyMenu.SetActive(true);
    }

    public void LoadGame()
    {
        Utility.LoadLevel("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
