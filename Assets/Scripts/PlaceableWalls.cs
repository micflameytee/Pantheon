using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaceableWalls : MonoBehaviour
{
    [SerializeField] private HealthSystem _leftWallHealth;
    [SerializeField] private HealthSystem _centerWallHealth;
    [SerializeField] private HealthSystem _rightWallHealth;

    
    public void WallHealth(int wallHealth)
    {
        _leftWallHealth.startingHealth = wallHealth;
        _centerWallHealth.startingHealth = wallHealth;
        _rightWallHealth.startingHealth = wallHealth;
        
        _leftWallHealth.ResetHealth();
        _centerWallHealth.ResetHealth();
        _rightWallHealth.ResetHealth();
    }
}
