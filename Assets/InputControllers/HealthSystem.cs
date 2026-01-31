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
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite UnDamagedSprite;
    [SerializeField] private Sprite DamagedSprite;
    [SerializeField] private Sprite moreDamagedSprite;
    [SerializeField] private Sprite alotMoreDamagedSprite;
    [SerializeField] private Sprite almostDestroyedSprite;
    [SerializeField] private Sprite DestroyedSprite;
    
    
    // Start is called before the first frame update
    public int startingHealth = 10;
    public int currentHealth;
    public AudioClip hitSound;
    private Statue _statue;

    private float damageCooldown = 0f;
    [SerializeField] private float MaxCooldown = 0.1f;
    
    private HealthType myType;
    
    private enum HealthType
    {
        player,
        statue,
        wall
    }

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
        if (CompareTag("Wall"))
        {
            myType = HealthType.wall;
        }
        else if (CompareTag("Angel"))
        {
            myType = HealthType.statue;
        }
        else if (CompareTag("Player"))
        {
            myType = HealthType.player;
        }
        
        
        _collider = GetComponent<Collider2D>();
        _statue = GetComponent<Statue>();
        currentHealth = startingHealth;
        if (IsWall)
        {
            _spriteRenderer.sprite = UnDamagedSprite;
            
            
            if (currentHealth >= 5)
            {
                //_spriteRenderer.color = new  Color32(255, 100, 100, 255);
            }
        }
        else if(IsStatue)
        {
            _spriteRenderer.sprite = UnDamagedSprite;
        }
    }


    private bool IsWall =>  myType == HealthType.wall;
    private bool IsStatue =>  myType == HealthType.statue;
    private bool IsPlayer =>  myType == HealthType.player;
    private bool IsDamaged => currentHealth < startingHealth;
    
    public void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int damage, PlayerController damageSource)
    {
        if (damageCooldown >= 0f)
        {
            return;
        }
        else
        {
            damageCooldown = MaxCooldown;
        }
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
        
        if ((IsWall || IsStatue) && IsDamaged && currentHealth > startingHealth / 6 * 5)
        {
            _spriteRenderer.sprite = DamagedSprite;
        } 
        else if (moreDamagedSprite != null && (IsWall || IsStatue) && IsDamaged && currentHealth > startingHealth / 6 * 4)
        {
            _spriteRenderer.sprite = moreDamagedSprite;
        }
        else if (alotMoreDamagedSprite != null && (IsWall || IsStatue) && IsDamaged && currentHealth > startingHealth / 6 * 3)
        {
            _spriteRenderer.sprite = alotMoreDamagedSprite;
        }
        else if (almostDestroyedSprite != null && (IsWall || IsStatue) && IsDamaged && currentHealth > startingHealth / 6 * 2)
        {
            _spriteRenderer.sprite = almostDestroyedSprite;
        }
        if (currentHealth <= 0)
        {
            OnDeath();
        }
        return currentHealth;
    }


    public void OnDeath()
    {
        
        if (IsWall)
        {
            _collider.enabled = false;
            _spriteRenderer.sprite = DestroyedSprite;
        } else if(IsStatue)
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
