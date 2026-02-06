using System;
using System.Collections;
using System.Collections.Generic;
using PlayerGods;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
{
    
    private Collider2D _collider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    [SerializeField] private HealthBar _healthBar;
    
    [SerializeField] private List<Sprite> SpriteStates;
    private int CurSpriteNum;
    
    
    // Start is called before the first frame update
    public int startingHealth = 10;
    public int currentHealth;
    public AudioClip hitSound;
    private Statue _statue;
    

    private float damageCooldown = 0f;
    [SerializeField] private float MaxCooldown = 0.1f;
    
    private HealthType myType;
    private PlayerController _player;

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
    public event Action<PlayerController> OnPlayerDamaged;
    
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

        if (IsPlayer)
        {
            Debug.Log($"is player");
        }
        
        if (!IsPlayer)
        {
            Debug.Assert(SpriteStates.Count != 0, "no sprite states for health system", this);
        }
        
        _collider = GetComponent<Collider2D>();
        _statue = GetComponent<Statue>();
        _player = GetComponent<PlayerController>();
        currentHealth = startingHealth;
        if (_healthBar != null)
        {
            _healthBar.SetHealth(startingHealth);
        }
        if (IsWall || IsStatue)
        {
            _spriteRenderer.sprite = SpriteStates[0];
        }
    }


    private bool IsWall =>  myType == HealthType.wall;
    private bool IsStatue =>  myType == HealthType.statue;
    private bool IsPlayer =>  myType == HealthType.player;
    private bool IsDamaged => currentHealth < startingHealth;
    public PlayerClassBase PlayerClass { get; set; }

    public void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

    public void TakeDamage(int damage, PlayerController damageSource)
    {
        damage = PlayerClass?.CalculateDamage(damage) ?? damage;
        
        
        if (damageCooldown >= 0f || damage <= 0)
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
        if (_healthBar != null)
        {
            _healthBar.SetHealth(currentHealth);
        }

        if (_player != null)
        {
            OnPlayerDamaged?.Invoke(_player);
        }

        SFX.Instance.PlaySound(hitSound, transform.position);
        //Debug.Log($"Player {name} has {currentHealth} / {startingHealth} health");
        CheckHealth();
    }

    public void ResetHealth()
    {
        currentHealth = startingHealth;
        if (_healthBar != null)
        {
            _healthBar.SetHealth(startingHealth);
        }
    }

    public int CheckHealth()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (!IsPlayer)
        {
            float damagePercent = (float)(startingHealth - currentHealth) / startingHealth;
            int SpriteCount = SpriteStates.Count;
            int currentHealthSprite = (int)((SpriteCount - 1) * damagePercent);
            _spriteRenderer.sprite = SpriteStates[currentHealthSprite];
        }

        if (currentHealth <= 0)
        {
            if (IsWall)
            {
                _collider.enabled = false;
            }
            OnDeath();
        }

        return currentHealth;
    }


    public void OnDeath()
    {

        if (_statue != null)
        {
            //Destroy(this.gameObject);
            _statue.SetRemoved();
        }
        else
        {
            OnPlayerDeath?.Invoke(_player);
        }
    }
}
