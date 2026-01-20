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
    private Statue _statue;

    public string Tag
    {
        get
        {
            return gameObject.tag;
        }
    }

    public event Action<PlayerController> OnPlayerDeath;
    
    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        _statue = GetComponent<Statue>();
        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(int damage, PlayerController damageSource)
    {
        if (_statue?.owner == damageSource)
        {
            Debug.Log($"This statue is owned by {name} has {CurrentHealth} / {StartingHealth} health");
            return;
        }
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
        
        if (CompareTag("Wall"))
        {
            Collider.enabled = false;
            _spriteRenderer.color = new  Color32(200, 200, 0, 255);
        }

        if (_statue != null)
        {
            //Destroy(this.gameObject);
            _statue.SetRemoved();
        }
        else
        {
            OnPlayerDeath?.Invoke(GetComponent<PlayerController>());
        }
    }
}
