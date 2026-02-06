using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject focusButton;
    
    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(focusButton);
    }

    public void LoadMainMenu()
    {
        Utility.LoadLevel("MainMenu");
    }
}
