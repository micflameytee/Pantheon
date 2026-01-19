using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSlotContainer : MonoBehaviour
{
    public EventSystem eventSystem;
    private ToggleGroup _toggleGroup;
    
    //[HideInInspector]
    private GameObject _firstSlot;

    public Transform mapSlotPrefab;

    public MapData[] maps;
    
    // NOTE: mapSlots needs to be initialized before MainMenu calls FocusFirst
    void Awake()
    {
        // Grab current EventSystem (from the Gameplay scene)
        eventSystem = EventSystem.current;
        
        // Kill all example / testing child map slots 
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        _toggleGroup = GetComponent<ToggleGroup>();

        for (int i = 0; i < maps.Length; i++)
        {
            Transform instance = Instantiate(mapSlotPrefab, transform);
            MapSlot mapSlot = instance.GetComponent<MapSlot>();
            
            // Apply map name, map mode and map icon to the map slot from the map data
            mapSlot.SetData(maps[i].mapName, maps[i].mapMode, maps[i].icon);
            mapSlot.SetToggleGroup(_toggleGroup);
            
            // Set the first slot 
            if (i == 0) { _firstSlot = mapSlot.gameObject; }
        }

        // Get all child MapSlots
        // _mapSlots = this.GetComponentsInChildren<MapSlot>();
    }

    void Start()
    {
        eventSystem.SetSelectedGameObject(_firstSlot);
    }
}
