using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClassInfoView : MonoBehaviour
{
    [SerializeField] private GameObject focusButton;
    
    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(focusButton);
    }

    public void LoadMainMenu()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("MainMenu");
    }
}
