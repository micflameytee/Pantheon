using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadLobby()
    {
        Utility.LoadLevel("Lobby");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
