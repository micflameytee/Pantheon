using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class ScreenScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float ScreenRatio = (float)Screen.height / (float)Screen.width;
        float ArenaRatio = 9f / 16f;
        

        if (ScreenRatio <= ArenaRatio)
        {
            Camera.main.orthographicSize = 4.5f;
        }
        else
        {
            float screenHeight = 4.5f * ScreenRatio / ArenaRatio;
            Camera.main.orthographicSize = screenHeight;   
        }
        
    }
}
