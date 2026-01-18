using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSlotContainer : MonoBehaviour
{
    private ToggleGroup toggleGroup;
    
    //[HideInInspector]
    private MapSlot[] mapSlots;
    
    // NOTE: mapSlots needs to be initialized before MainMenu calls FocusFirst
    void Awake()
    {
        toggleGroup = GetComponent<ToggleGroup>();

        // Get all child MapSlots
        mapSlots = this.GetComponentsInChildren<MapSlot>();

        // Set each child toggle.group parameter to this toggle group
        foreach (MapSlot mapSlot in mapSlots)
        {
            mapSlot.SetToggleGroup(toggleGroup);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FocusFirst(EventSystem eventSystem)
    {
        eventSystem.SetSelectedGameObject(mapSlots[0].gameObject);
    }
}
