using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recall : MonoBehaviour
{
    
    
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _recallSprite;
    [SerializeField] private Sprite _usedRecallSprite;


    public void SetExpired(bool isExpired)
    {
        _spriteRenderer.sprite = isExpired ? _recallSprite : _usedRecallSprite;
    }
}
