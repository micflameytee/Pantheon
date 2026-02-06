using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomRules : MonoBehaviour
{
    [SerializeField] private GameObject focusButton;
    [SerializeField] private Toggle godsEnabledToggle;
    [SerializeField] private Toggle annoyingGhostsToggle;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(focusButton);
        SetGodsEnabled(PlayerPrefs.GetInt("GodsEnabled", 1));
        SetAnnoyingGhosts(PlayerPrefs.GetInt("AnnoyingGhosts", 0));
    }

    void SetGodsEnabled(int value)
    {
        if (value == 0)
        {
            godsEnabledToggle.isOn = false;
        }
        else
        {
            godsEnabledToggle.isOn = true;
        }
        PlayerPrefs.SetInt("GodsEnabled", value);
    }

    public void SetCustomGodsToggle()
    {
        SetGodsEnabled(godsEnabledToggle.isOn ? 1 : 0);
    }
    
    void SetAnnoyingGhosts(int value)
    {
        if (value == 0)
        {
            annoyingGhostsToggle.isOn = false;
        }
        else
        {
            annoyingGhostsToggle.isOn = true;
        }
        PlayerPrefs.SetInt("AnnoyingGhosts", value);
    }

    public void SetAnnoyingGhostsToggle()
    {
        SetAnnoyingGhosts(annoyingGhostsToggle.isOn ? 1 : 0);
    }
    
    public void ExitCustomRules()
    {
        SFX.Instance.PlaySound("ui_select");
        Utility.LoadLevel("MainMenu");
        // Utility.LoadLevel("GameplayScene");
        // Utility.UnloadLevel("CustomRules");
    }
}
