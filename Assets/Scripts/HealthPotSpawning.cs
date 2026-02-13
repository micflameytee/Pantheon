using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotSpawning : MonoBehaviour
{
    [SerializeField] private List<Transform> placementLocations;
    [SerializeField] private HealthPotion Potion;

    [SerializeField] private float placeFrequency = 30f;
    private float curTime = 0f;
    
    private void Awake()
    {
        curTime = placeFrequency;
        PlacePotions();
    }

    public void PlacePotions()
    {
        foreach (Transform placementLocation in placementLocations)
        {
            if (Random.Range(0, 100) > 90)
            {
                var newInstance = Instantiate(Potion, placementLocation.position,  Quaternion.identity);
            }
        }
    }

    
    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime < 0)
        {
            curTime = placeFrequency;
            PlacePotions();
        }
    }
}
