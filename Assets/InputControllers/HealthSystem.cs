using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    
    private Collider2D _collider;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    
    
    // Start is called before the first frame update
    public int startingHealth = 10;
    public int currentHealth;
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
        _collider = GetComponent<Collider2D>();
        _statue = GetComponent<Statue>();
        currentHealth = startingHealth;
        
        if (CompareTag("Wall") && currentHealth >= 5)
        {
            _spriteRenderer.color = new  Color32(200, 100, 200, 255);
        }
    }

    public void TakeDamage(int damage, PlayerController damageSource)
    {
        if (_statue?.owner == damageSource)
        {
            Debug.Log($"This statue is owned by {name} has {currentHealth} / {startingHealth} health");
            return;
        }
        currentHealth -= damage;
        Debug.Log($"Player {name} has {currentHealth} / {startingHealth} health");
        
        
        CheckHealth();
    }

    public int CheckHealth()
    {
        
        if (CompareTag("Wall") && currentHealth == 1)
        {
            _spriteRenderer.color = new  Color32(200, 100, 0, 255);
        }
        if (currentHealth <= 0)
        {
            OnDeath();
        }
        return currentHealth;
    }

    public void OnDeath()
    {
        
        if (CompareTag("Wall"))
        {
            _collider.enabled = false;
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
