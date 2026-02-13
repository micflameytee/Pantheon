using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour
{
    
    
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _recallSprite;
    [SerializeField] private Sprite _usedRecallSprite;
    private bool cooldownActive = false;
    private float destroyCountdown = 11f;


    public void SetExpired(bool isExpired)
    {
        _spriteRenderer.sprite = !isExpired ? _recallSprite : _usedRecallSprite;
        cooldownActive = isExpired;
    }


    private void Update()
    {
        if (cooldownActive)
        {
            destroyCountdown -= Time.deltaTime;
        }
        
        if (destroyCountdown <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
