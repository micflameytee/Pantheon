using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamemodeContainer : MonoBehaviour
{
    public TMP_Dropdown gamemode;
    public TMP_Text description;
    
    // Start is called before the first frame update
    void Start()
    {
        gamemode.ClearOptions();
        gamemode.AddOptions(MapData.GetMapModeStrings());
        UpdateDescription();
    }

    public void UpdateDescription()
    {
        description.text = MapData.GetMapModeDescription((MapData.MapMode) gamemode.value);
    }
}
