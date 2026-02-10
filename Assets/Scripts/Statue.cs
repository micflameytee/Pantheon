using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [HideInInspector] public HealthSystem owner;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite UnDamagedSprite;
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private Transform[] spawnPoints;
    
    public bool isStillThere = true;
    
    private int _currentSpawnPointIndex = 0;

    private void Awake()
    {
        //_spriteRenderer.sprite = UnDamagedSprite;
    }

    public Transform GetSpawnPoint()
    {
        Transform spawnPoint = spawnPoints[_currentSpawnPointIndex];
        _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % spawnPoints.Length;
        return spawnPoint;
    }

    public void SetRemoved()
    {
        isStillThere = false;
        owner.ResetHealth();
        
        // change sprite to crumbled statue
        // _spriteRenderer.sprite = destroyedSprite;
    }

    public bool StillThere()
    {
        return isStillThere;
    }
}
