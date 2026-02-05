using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomRules : MonoBehaviour
{
    [SerializeField] private GameObject focusButton;
    [SerializeField] private Toggle customGodsToggle;
    [SerializeField] private Toggle annoyingGhostsToggle;

    void Awake()
    {
        EventSystem.current.SetSelectedGameObject(focusButton);
        SetCustomGods(PlayerPrefs.GetInt("CustomGods", 0));
        SetAnnoyingGhosts(PlayerPrefs.GetInt("AnnoyingGhosts", 0));
    }

    void SetCustomGods(int value)
    {
        if (value == 0)
        {
            customGodsToggle.isOn = false;
        }
        else
        {
            customGodsToggle.isOn = true;
        }
        PlayerPrefs.SetInt("CustomGods", value);
    }

    public void SetCustomGodsToggle()
    {
        SetCustomGods(customGodsToggle.isOn ? 1 : 0);
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
        Utility.UnloadLevel("CustomRules");
    }
}
