using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BearTrap : MonoBehaviour
{
    public PlayerController owner { get; set; }
    [SerializeField] private SpriteRenderer GlueSprite;
    [SerializeField] private Sprite driedGlue;
    private PlayerController _otherController;
    private float _movementCooldown = 5f;
    [SerializeField]private float MaxCooldown = 5f;
    private bool cooldownActive = false;
    private float destroyCountdown = 10f;
    private bool destroyActive = false;
    
    [SerializeField] private AudioClip deploySfx;


    private void Awake()
    {
        destroyCountdown += MaxCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"bearTrap");
        _otherController = other.GetComponent<PlayerController>();
        if (_otherController.GetGhost())
        {
            //Debug.Log($"dead");
            return;
        }
        if (_otherController != null && other.CompareTag("Player") && owner != _otherController)
        {
            SFX.Instance.PlaySound(deploySfx, transform.position);
            //Debug.Log($"other player");
            cooldownActive = true;
            _movementCooldown = MaxCooldown;
            _otherController.CanMove = false;
            GetComponent<Collider2D>().enabled = false;
            destroyActive = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownActive)
        {
            _movementCooldown -= Time.deltaTime;
        }
        
        if (_movementCooldown <= 0f)
        {
            GlueSprite.sprite = driedGlue;
            cooldownActive = false;
            _otherController.CanMove = true;
            _movementCooldown = MaxCooldown;
        }

        if (destroyActive)
        {
            destroyCountdown -= Time.deltaTime;
        }
        if (destroyCountdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
