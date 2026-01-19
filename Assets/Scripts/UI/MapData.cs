using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Map Data", fileName = "Map")]    
public class MapData : ScriptableObject
{
    // The name of the actual scene to load
    public string sceneName = "SceneName";
    
    // The name of the map
    public string mapName = "Awesome Map";
    
    public Sprite icon;
    
    public enum MapMode {FFA, _2V2}
    public MapMode mapMode = MapMode.FFA;
    
    // Player ranges
    [Range(2, 8)]
    public int playerMin = 2;

    [Range(2, 8)]
    public int playerMax = 2;
}
