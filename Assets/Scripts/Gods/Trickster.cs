using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trickster : MonoBehaviour
{
    
    public Gods owner { get; set; }
    
    
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _recallSprite;
    [SerializeField] private Sprite _usedRecallSprite;
    
    private float _recallCooldown = 1f;
    public float recallWaitTime = 5f;


    private void Awake()
    {
        _recallCooldown = recallWaitTime;
        Debug.Log($"triggered");
        _spriteRenderer.sprite = _recallSprite;
    }

    // Update is called once per frame
    void Update()
    {
        _recallCooldown -= Time.deltaTime;
        
        if (_recallCooldown <= 0f)
        {
            owner.transform.position = this.transform.position;
            _spriteRenderer.sprite = _usedRecallSprite;
            _recallCooldown = recallWaitTime * 10000f;
        }
    }
}
