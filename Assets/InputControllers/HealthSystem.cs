using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    
    private Collider2D Collider;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    public int StartingHealth = 10;
    private int CurrentHealth;
    
    public String Tag;

    public event Action<PlayerController> OnPlayerDeath;
    
    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
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
        if (tag != null && tag == "Wall" && CurrentHealth == 1)
        {
            _spriteRenderer.color = new  Color32(200, 100, 0, 255);
        }
        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
        return CurrentHealth;
    }

    public void OnDeath()
    {
        if (Tag != null && Tag == "Wall")
        {
            Collider.enabled = false;
            _spriteRenderer.color = new  Color32(200, 200, 0, 255);
        }
        OnPlayerDeath?.Invoke(GetComponent<PlayerController>());
    }
}
