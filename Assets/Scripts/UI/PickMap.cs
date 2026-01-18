using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickMap : MonoBehaviour
{
    public EventSystem eventSystem;
    public MapSlotContainer mapSlotContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        mapSlotContainer.FocusFirst(eventSystem);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
