using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public Statue ownedStatue { get; set; }
    
    
    [SerializeField]private DamageSystem _damageSystem;
    [SerializeField]private HealthSystem _healthSystem;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    public HealthSystem HealthSystem => _healthSystem;
    private bool _isGhost;
    private float RespawnCooldown { get; set; }
    public float respawnCooldownMax = 3f;
    
    private Rigidbody2D rb;
    
    private static int playerNumber;

    public float MoveSpeed = 10f;
    private Vector2 _moveDirection = Vector2.zero;
    
    
    public void HandleMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        if (_isGhost)
            return;
        _damageSystem.PreformAttack();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RespawnCooldown = 100000000000f;
    }

    private void Awake()
    {
        _damageSystem.Initialize(this);
        playerNumber++;
        name = $"Player {playerNumber}";
    }

    private void Update()
    {
        Quaternion oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        
        transform.rotation = oldRotation;
        
        RespawnCooldown -= Time.deltaTime;

        if (_isGhost && RespawnCooldown <= 0f)
        {
            //player.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            SetGhost(false);
            transform.position = ownedStatue.GetSpawnPoint().position;
        }
        

        if (_moveDirection.sqrMagnitude > 0f)
        {
            // convert the _moveDirection into a 2d rotation direction
            float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            //Debug.Log(transform.rotation.eulerAngles.y);
        }
    }
    
    void FixedUpdate() {
            rb.velocity = _moveDirection * MoveSpeed;
    }

    public void ResetCooldown()
    {
        RespawnCooldown = 0f;
    }

    public void SetGhost(bool isGhost)
    {
        if (isGhost && ownedStatue != null && ownedStatue.StillThere())
        {
            RespawnCooldown = respawnCooldownMax;
        }
        _isGhost = isGhost;
        _spriteRenderer.color = isGhost 
            ? new  Color32(50, 255, 50, 128)
            : new  Color32(255, 255, 255, 255);;
    }
}
