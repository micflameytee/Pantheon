using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int StartingHealth = 10;
    private int CurrentHealth;

    public event Action<PlayerController> OnPlayerDeath;
    
    private void Awake()
    {
        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"Player {name} has {CurrentHealth} / {StartingHealth} health");
        CheckHealth();
    }

    public int CheckHealth()
    {
        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
        return CurrentHealth;
    }

    public void OnDeath()
    {
        OnPlayerDeath?.Invoke(GetComponent<PlayerController>());
    }
}
