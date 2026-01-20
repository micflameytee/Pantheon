using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Data", fileName = "Map")]    
public class MapData : ScriptableObject
{
    // The name of the actual scene to load
    public string sceneName = "SceneName";
    
    // The name of the map
    public string mapName = "Map Name";

    public Sprite icon;
    
    public enum MapMode {NORMAL, GODLESS, DEATHMATCH, GODLESS_DEATHMATCH}

    public MapMode[] supportedModes;

    // Get the map mode as a string
    public static string GetMapModeString(MapMode mapMode)
    {
        switch (mapMode)
        {
            case MapMode.NORMAL:
                return "Normal";
            case MapMode.GODLESS:
                return "Godless";
            case MapMode.DEATHMATCH:
                return "Deathmatch";
            case MapMode.GODLESS_DEATHMATCH:
                return "Godless Deathmatch";
            default:
                Debug.LogWarning("Unknown map mode: " + mapMode);
                return "Unknown map mode";
        }
    }

    // Get all map modes as strings
    public static List<String> GetMapModeStrings()
    {
        List<String> mapModeStrings = new List<string>();
        
        var values = Enum.GetValues(typeof(MapMode));

        foreach (MapMode value in values)
        {
            mapModeStrings.Add(GetMapModeString(value));
        }
        
        return mapModeStrings;
    }

    // Get the map mode description
    public static string GetMapModeDescription(MapMode mapMode)
    {
        switch (mapMode)
        {
            case MapMode.NORMAL:
                return "Statues and gods";
            case MapMode.GODLESS:
                return "Statues but no gods";
            case MapMode.DEATHMATCH:
                return "Gods but no statues";
            case MapMode.GODLESS_DEATHMATCH:
                return "No statues, no gods";
            default:
                Debug.LogWarning("Unknown map mode: " + mapMode);
                return "Unknown map mode";
        }
    }
    
    // Player ranges
    [Range(2, 8)] public int playerMin = 2;

    [Range(2, 8)] public int playerMax = 2;
    
    // Getters and setters
    // These need to be added manually - they vanish in the editor with the { get, set } trick
    public string GetSceneName() { return sceneName; }
    public string GetMapName() { return mapName; }
    
    public Sprite GetIcon() { return icon; }
    
    public MapMode[] GetSupportedModes() { return supportedModes; }
    
    public int GetPlayerMin() {
        return playerMin;
    }
    
    public int GetPlayerMax() {
        return playerMax;
    }
}
