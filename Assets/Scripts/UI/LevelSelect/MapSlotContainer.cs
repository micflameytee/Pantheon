using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSlotContainer : MonoBehaviour
{
    private GlobalPlayerManager _globalPlayerManager;
    private ToggleGroup _toggleGroup;

    public Transform mapSlotPrefab;

    public MapData[] maps;
    private List<GameObject> _mapSlots = new List<GameObject>();
    
    public TMP_Dropdown gamemode;
    public Button playButton;
    
    [HideInInspector]
    public string currentSelectedScene; 
    
    // NOTE: mapSlots needs to be initialized before MainMenu calls FocusFirst
    void Awake()
    {
        // Grab current EventSystem (from the Gameplay scene)
        // eventSystem = EventSystem.current;
        
        _globalPlayerManager = FindObjectOfType<GlobalPlayerManager>();
        
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
            
            Toggle toggle = instance.GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate { OnMapSlotSelected(mapSlot); });
            
            // Apply map name, map mode and map icon to the map slot from the map data
            mapSlot.SetData(maps[i].sceneName, maps[i].mapName, maps[i].supportedModes, maps[i].playerMin, maps[i].playerMax, maps[i].icon);
            mapSlot.SetToggleGroup(_toggleGroup);
            
        }
    }

    void Start()
    {
        UpdateFiltering();
    }

    // Update visibility of MapSlots based on which category is selected
    public void UpdateFiltering()
    {
        int count = _globalPlayerManager.players.Count;
        // Debug.Log(count);
        
        foreach (GameObject mapSlot in _mapSlots)
        {
            MapSlot mapSlotScript = mapSlot.GetComponent<MapSlot>();
            
            // Filter out maps if unsupported gamemode
            if (mapSlotScript.IsSupportedMode((MapData.MapMode) gamemode.value))
            {
                mapSlot.SetActive(true);
            }
            else
            {
                mapSlot.SetActive(false);
            }
            
            // Filter out maps if unsupported player count
            if (count >= mapSlotScript.playerMin && count <= mapSlotScript.playerMax)
            {
                // Debug.Log("Allowing " + mapSlotScript.sceneName);
            }
            else
            {
                // Debug.Log("Removing " + mapSlotScript.sceneName);
                mapSlot.SetActive(false);
            }
        }
        
        UpdatePlayButton();
    }

    public void OnMapSlotSelected(MapSlot mapSlot)
    {
        currentSelectedScene = mapSlot.sceneName;
        UpdatePlayButton();
    }

    // Checks whether the play button needs to be disabled (no valid map selected) and updates it accordingly
    void UpdatePlayButton()
    {
        playButton.interactable = false;
        foreach (Toggle toggle in _toggleGroup.ActiveToggles())
        {
            if (toggle.gameObject.activeInHierarchy)
            {
                playButton.interactable = true;
                Debug.Log(toggle.gameObject.name + " is active");
            }
        }
    }
}
