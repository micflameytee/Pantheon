using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GamemodeContainer : MonoBehaviour
{
    public TMP_Dropdown gamemode;
    public TMP_Text description;
    
    private EventSystem _eventSystem;
    public GameObject gamemodeDropdown;

    void Awake()
    {
        _eventSystem = EventSystem.current;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        gamemode.ClearOptions();
        gamemode.AddOptions(MapData.GetMapModeStrings());
        UpdateDescription();
        
        _eventSystem.SetSelectedGameObject(gamemodeDropdown);
    }

    public void UpdateDescription()
    {
        description.text = MapData.GetMapModeDescription((MapData.MapMode) gamemode.value);
    }
}
