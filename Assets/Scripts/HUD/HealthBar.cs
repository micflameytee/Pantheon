using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform healthSpritePrefab;
    private List<GameObject> _sprites = new List<GameObject>();
    
    public int maxHealth;
    public int currentHealth;
    
    void Awake()
    {
        // Kill all preview sprites 
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
        for (int i = 0; i < maxHealth; i++)
        {
            Transform instance = Instantiate(healthSpritePrefab, transform);
            // Shift each sprite by 0.2 on X axis
            instance.position = new Vector3(i * 0.2f, 0, 0);
            _sprites.Add(instance.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetHealth(int health)
    {
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
