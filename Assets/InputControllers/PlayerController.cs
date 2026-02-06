using System;
using System.Collections;
using System.Collections.Generic;
using PlayerGods;
using UI.PlayerHud;
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

    [SerializeField] private AudioClip bombReloadSfx;
    [SerializeField] private AudioClip glueReloadSfx;
    [SerializeField] private AudioClip bombSetSfx;
    [SerializeField] private AudioClip glueSetSfx;
    
    [SerializeField] private TrapBar trapBar;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private GameObject trapBarPrefab;
    
    public Bomb bomb;
    public BearTrap bearTrap;

    public bool CanMove { get; set; } = true;

    private Rigidbody2D _rb;
    
    private static int _playerNumber;

    public float moveSpeed = 5f;
    private Vector2 _moveDirection = Vector2.zero;
    public AudioClip damageSound;
    private bool _isLobbyMode;
    public PlayerInfo InfoPanel { get; private set; }


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
        
        SFX.Instance.PlaySound(bombSetSfx, transform.position);
        trapBar.SetBomb(false);
        StartCoroutine(WaitThenReload(bombCooldown, bombReloadSfx, TrapBar.TrapType.Bomb));
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
        
        SFX.Instance.PlaySound(glueSetSfx, transform.position);
        trapBar.SetGlue(false);
        StartCoroutine(WaitThenReload(glueTrapCooldown, glueReloadSfx, TrapBar.TrapType.Glue));
    }

    public void HandleAbility(float seconds, AudioClip reloadSfx)
    {
        trapBar.SetAbility(false);
        StartCoroutine(WaitThenReload(seconds, reloadSfx, TrapBar.TrapType.Ability));
    }

    private IEnumerator WaitThenReload(float seconds, AudioClip sound, TrapBar.TrapType trapType)
    {
        yield return new WaitForSeconds(seconds);
        if (sound != null) { SFX.Instance.PlaySound(sound, transform.position); }
        
        switch (trapType)
        {
            case TrapBar.TrapType.Bomb:
                trapBar.SetBomb(true);
                break;
            case TrapBar.TrapType.Glue:
                trapBar.SetGlue(true);
                break;
            case TrapBar.TrapType.Ability:
                trapBar.SetAbility(true);
                break;
        }
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

    public void Reset()
    {
        _playerNumber = 0;
    }

    private void HandlePlayerClassChanged(PlayerClassBase playerClass)
    {
        if (playerClass.PlayerSprite != null)
        {
            _spriteRenderer.sprite = playerClass.PlayerSprite;
        }

        SetAnnoyingGhost(PlayerPrefs.GetInt("AnnoyingGhosts", 0));
    }

    private void SetAnnoyingGhost(int playerPrefValue)
    {
        if (playerPrefValue == 0)
        {
            annoyingGhost = false;
        }
        else
        {
            annoyingGhost = true;
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
            
            healthBarPrefab.SetActive(false);
            trapBarPrefab.SetActive(false);
        }
        else
        {
//            Debug.Log($"index: {_playerNumber} \n size: {_spriteColours.Length}");
            _spriteRenderer.sprite = _spriteColours[playerNumber];
            _god.normalSprite = _spriteColours[playerNumber];
            
            healthBarPrefab.SetActive(true);
            trapBarPrefab.SetActive(true);
        }
        
        if (isGhost && OwnedStatue !=null && OwnedStatue.StillThere())
        {
            SFX.Instance.PlaySound("ui_chime", transform.position);
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

        if (isGhost)
        {
            if (annoyingGhost)
            {
                GetComponent<Collider2D>().enabled = true;
            } else {
                GetComponent<Collider2D>().enabled = false;
            }
        } else {
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

    public void RegisterInfoPanel(PlayerInfo playerInfoPanel)
    {
        InfoPanel = playerInfoPanel;
    }
}
