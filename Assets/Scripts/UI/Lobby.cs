using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Lobby : MonoBehaviour
{

    public void LoadPickMap()
    {
        Debug.Log("Loading level");
        Utility.LoadLevel("PickMap");
    }
}
