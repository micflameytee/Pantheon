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
    [SerializeField] private Sprite UnDamagedSprite;
    [SerializeField] private Sprite DamagedSprite;
    [SerializeField] private Sprite DestroyedSprite;
    
    
    // Start is called before the first frame update
    public int startingHealth = 10;
    public int currentHealth;
    public AudioClip hitSound;
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
        if (CompareTag("Wall"))
        {
            _spriteRenderer.sprite = UnDamagedSprite;
            
            
            if (currentHealth >= 5)
            {
                //_spriteRenderer.color = new  Color32(255, 100, 100, 255);
            }
        } else if(CompareTag("Angel"))
        {
            _spriteRenderer.sprite = UnDamagedSprite;
        }
    }

    public void TakeDamage(int damage, PlayerController damageSource)
    {
        if (_statue != null && _statue.owner !=null && _statue?.owner == damageSource)
        {
            Debug.Log($"This statue is owned by {name} has {currentHealth} / {startingHealth} health");
            return;
        }
        currentHealth -= damage;
        SFX.Instance.PlaySound(hitSound, transform.position);
        Debug.Log($"Player {name} has {currentHealth} / {startingHealth} health");
        CheckHealth();
    }

    public int CheckHealth()
    {
        
        if (CompareTag("Wall") && currentHealth == startingHealth / 2)
        {
            _spriteRenderer.sprite = DamagedSprite;
            //_spriteRenderer.color = new  Color32(200, 100, 0, 255);
        } else if(CompareTag("Angel") && currentHealth == startingHealth / 2)
        {
            _spriteRenderer.sprite = DamagedSprite;
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
            _spriteRenderer.sprite = DestroyedSprite;
            //_spriteRenderer.color = new  Color32(200, 200, 0, 255);
        } else if(CompareTag("Angel"))
        {
            _spriteRenderer.sprite = DestroyedSprite;
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
