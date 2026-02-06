using System;
using System.Collections;
using System.Collections.Generic;
using PlayerGods;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Statue OwnedStatue { get; set; }
    
    [SerializeField]private DamageSystem _damageSystem;
    [SerializeField]private HealthSystem _healthSystem;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    [SerializeField]private Sprite[] _spriteColours;
    [SerializeField]private Sprite ghostSprite;
    [SerializeField] private Gods _god;
    public Gods God => _god;
    

    
    
    
    
    
    
    private int playerNumber = 0;
    
    public HealthSystem HealthSystem => _healthSystem;
    private bool _isGhost;
    public bool annoyingGhost;
    
    
    private float RespawnCooldown { get; set; }
    public float respawnCooldownMax = 3f;
    private float CurrentGlueTrapCooldown = 0f;
    private float CurrentBombCooldown = 0f;
    public float glueTrapCooldown = 1f;
    public float bombCooldown = 10f;

    
    
    
    public Bomb bomb;
    public BearTrap bearTrap;

    public bool CanMove { get; set; } = true;

    private Rigidbody2D _rb;
    
    private static int _playerNumber;

    public float moveSpeed = 5f;
    private Vector2 _moveDirection = Vector2.zero;
    public AudioClip damageSound;
    private bool _isLobbyMode;


    public void HandleMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;
        if (_isLobbyMode)
        {
            _god.ChangeGod();
            return;
        }
        
        if (_isGhost)
            return;
        _damageSystem.PreformAttack();
    }

    public void HandleBomb(InputAction.CallbackContext context)
    {
        if (CurrentBombCooldown > 0 || _isGhost)
        {
            return;
        }
        var newInstance = Instantiate(bomb, transform.position, transform.rotation);
        newInstance.owner = this;
        CurrentBombCooldown = bombCooldown;
    }

    public void HandleBearTrap(InputAction.CallbackContext context)
    {
        if (CurrentGlueTrapCooldown > 0 || _isGhost)
        {
            return;
        }
        var newInstance = Instantiate(bearTrap, transform.position, transform.rotation);
        newInstance.owner = this;
        CurrentGlueTrapCooldown = glueTrapCooldown;
    }

    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        RespawnCooldown = 100000000000f;
    }

    private void Awake()
    {
        _damageSystem.Initialize(this);
        _playerNumber++;
        name = $"Player {_playerNumber}";
        God.OnPlayerClassChanged += HandlePlayerClassChanged;
    }

    private void HandlePlayerClassChanged(PlayerClassBase playerClass)
    {
        if (playerClass.PlayerSprite != null)
        {
            _spriteRenderer.sprite = playerClass.PlayerSprite;
        }
    }

    private void Update()
    {

        Quaternion oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        
        transform.rotation = oldRotation;
        
        CurrentGlueTrapCooldown -= Time.deltaTime;
        CurrentBombCooldown -= Time.deltaTime;
        RespawnCooldown -= Time.deltaTime;

        if (_isGhost && RespawnCooldown <= 0f)
        {
            //player.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            SetGhost(false);
            transform.position = OwnedStatue.GetSpawnPoint().position;
        }
        
        if (_moveDirection.sqrMagnitude > 0f)
        {
            // convert the _moveDirection into a 2d rotation direction
            float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
    }

    public void SetPlayerColour(int curPlayerNumber)
    {
        this.playerNumber = curPlayerNumber;
    }
    
    void FixedUpdate() {
        if (!CanMove)
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        _rb.velocity = _moveDirection * moveSpeed * _god.CurrentPlayerClass.SpeedMultiplier;
    }

    public void ResetCooldown()
    {
        RespawnCooldown = 0f;
    }

    public void SetGhost(bool isGhost)
    {
        if (isGhost)
        {
            _spriteRenderer.sprite = ghostSprite;
        }
        else
        {
            Debug.Log($"index: {_playerNumber} \n size: {_spriteColours.Length}");
            _spriteRenderer.sprite = _spriteColours[playerNumber];
            _god.normalSprite = _spriteColours[playerNumber];
        }
        if (isGhost && OwnedStatue !=null && OwnedStatue.StillThere())
        {
            RespawnCooldown = respawnCooldownMax;
        }
        else
        {
            RespawnCooldown = 86400f;
            _healthSystem.ResetHealth();
        }
        _isGhost = isGhost;
        _spriteRenderer.color = isGhost 
            ? new  Color32(255, 255, 255, 127)
            : new  Color32(255, 255, 255, 255);
        
        if (!annoyingGhost && _isGhost)
        {
            GetComponent<Collider2D>().enabled = false;
        }
        else if (!annoyingGhost && !_isGhost)
        {
            GetComponent<Collider2D>().enabled = true;
        }
    }

    public bool GetGhost()
    {
        return _isGhost;
    }

    public bool GetLobbyMode()
    {
        return _isLobbyMode;
    }

    public void SetLobbyMode(bool isLobbyMode)
    {
        _isLobbyMode = isLobbyMode;
    }
}
