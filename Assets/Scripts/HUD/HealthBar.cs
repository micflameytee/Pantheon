using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public List<GameObject> _sprites = new List<GameObject>();
    
    
    void Awake()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetHealth(int health)
    {
        if (health <= 0)
        {
            foreach (GameObject sprite in _sprites)
            {
                sprite.SetActive(false);
            }

            return;
        }
        
        switch (health)
        {
            case 1:
                _sprites[0].SetActive(true);
                _sprites[1].SetActive(false);
                _sprites[2].SetActive(false);
                break;
            case 2:
                _sprites[0].SetActive(true);
                _sprites[1].SetActive(true);
                _sprites[2].SetActive(false);
                break;
            case 3:
                foreach (GameObject sprite in _sprites)
                {
                    sprite.SetActive(true);
                }
                break;
            default:
                break;
        }
    }
}
