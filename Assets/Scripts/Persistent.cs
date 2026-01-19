using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    // Any object this script is attached to will persist between scene loads
    // Use with caution!
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
