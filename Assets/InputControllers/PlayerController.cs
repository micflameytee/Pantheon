using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Statue ownedStatue { get; set; }
    
    
    [SerializeField]private DamageSystem _damageSystem;
    [SerializeField]private HealthSystem _healthSystem;
    [SerializeField]private SpriteRenderer _spriteRenderer;
    public HealthSystem HealthSystem => _healthSystem;
    private bool _isGhost;
    public float Cooldown { get; set; }
    
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
        Cooldown = 0f;
    }

    private void Awake()
    {
        _damageSystem.Initialize(_healthSystem);
        playerNumber++;
        name = $"Player {playerNumber}";
    }

    private void Update()
    {
        Quaternion oldRotation = transform.rotation;
        transform.rotation = Quaternion.identity;
        
        transform.rotation = oldRotation;
        
        Cooldown -= Time.deltaTime;
        
        

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
    

    public void SetGhost(bool isGhost)
    {
        if (GetComponent<PlayerController>().ownedStatue.stillThere())
        {
            Cooldown = 3f;
        }
        _isGhost = isGhost;
        _spriteRenderer.color = isGhost 
            ? new  Color32(50, 255, 50, 128)
            : new  Color32(255, 255, 255, 255);;
    }
}
