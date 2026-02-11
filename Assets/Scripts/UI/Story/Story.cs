using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public void LoadMainMenu()
    {
        PlayerPrefs.SetInt("StoryEnabled", 0);
        Utility.LoadLevel("MainMenu");
    }
}
