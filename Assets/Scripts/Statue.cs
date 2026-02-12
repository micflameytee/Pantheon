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
    [SerializeField] private HealthPotSpawning _healthSpawner;
    public bool isStillThere = true;

    private float curTimer = 0f;
    private float startTimer = 300f;
    
    private int _currentSpawnPointIndex = 0;

    private void Awake()
    {
        curTimer = startTimer;
    }

    private void Update()
    {
        curTimer -= Time.deltaTime;
        if (curTimer <= 0f)
        {
            this.GetComponent<HealthSystem>().TakeDamage(2, null);
            curTimer = startTimer;
        }
    }

    public Transform GetSpawnPoint()
    {
        Transform spawnPoint = spawnPoints[_currentSpawnPointIndex];
        _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % spawnPoints.Length;
        return spawnPoint;
    }

    public void SetRemoved()
    {
        _healthSpawner.PlacePotions();
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
