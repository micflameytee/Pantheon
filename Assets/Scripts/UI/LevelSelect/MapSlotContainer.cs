using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSlotContainer : MonoBehaviour
{
    public EventSystem eventSystem;
    private ToggleGroup _toggleGroup;

    public Transform mapSlotPrefab;

    public MapData[] maps;
    private List<GameObject> _mapSlots = new List<GameObject>();
    
    public TMP_Dropdown gamemode;
    
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
            _mapSlots.Add(instance.gameObject);
            
            // Apply map name, map mode and map icon to the map slot from the map data
            mapSlot.SetData(maps[i].mapName, maps[i].supportedModes, maps[i].icon);
            mapSlot.SetToggleGroup(_toggleGroup);
            
        }

        // Get all child MapSlots
        // _mapSlots = this.GetComponentsInChildren<MapSlot>();
    }

    void Start()
    {
        UpdateFiltering();
        Debug.Log(transform.GetChild(0).gameObject);
        eventSystem.SetSelectedGameObject(transform.GetChild(0).gameObject);
    }

    // Update visibility of MapSlots based on which category is selected
    public void UpdateFiltering()
    {
        foreach (GameObject mapSlot in _mapSlots)
        {
            MapSlot mapSlotScript = mapSlot.GetComponent<MapSlot>();

            if (mapSlotScript.IsSupportedMode((MapData.MapMode) gamemode.value))
            {
                mapSlot.SetActive(true);
            }
            else
            {
                mapSlot.SetActive(false);
            }
            
        }
    }
}
