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
        //gamemode.AddOptions(MapData.GetMapModeStrings());
        // TODO: NOTE: Temporarily disabled other map modes here, uncomment previous line to enable
        // <fudge>
        List <string> options = new List<string>();
        //options.Add(MapData.GetMapModeString(MapData.MapMode.GODLESS));
        //options.Add(MapData.GetMapModeString(MapData.MapMode.GODLESS_DEATHMATCH));
        
        options.Add("Standard");
        options.Add("Deathmatch");
        // </fudge>
        
        gamemode.AddOptions(options);
        UpdateDescription();
        
        _eventSystem.SetSelectedGameObject(gamemodeDropdown);
    }

    public void UpdateDescription()
    {
        // description.text = MapData.GetMapModeDescription((MapData.MapMode) gamemode.value);
        // TODO: NOTE: Temporarily disabled other map modes here, uncomment previous line to enable
        // <fudge>
        if (gamemode.value == 0)
        {
            // description.text = MapData.GetMapModeDescription(MapData.MapMode.GODLESS);
            description.text = "Break opponents statues, then kill them to win";
        }
        else
        {
            description.text = "One life, last person standing wins";
        }
        // </fudge>
        
    }
}
