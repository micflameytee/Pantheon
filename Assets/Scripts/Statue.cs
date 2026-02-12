using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [HideInInspector] public List<HealthSystem> owner;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite UnDamagedSprite;
    [SerializeField] private Sprite destroyedSprite;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private HealthPotSpawning _healthSpawner;
    public bool isStillThere = true;

    [SerializeField] private StatueDecayList statueDecayList;
    private StatueDecay _statueDecayOption;
    private HealthSystem _healthSystem;
    private float curTimer;
    private float startTimer;
    
    private int _currentSpawnPointIndex = 0;

    private void Awake()
    {
        _healthSystem = this.GetComponent<HealthSystem>();
        
        _statueDecayOption = statueDecayList.list[PlayerPrefs.GetInt("StatueDecay", 0)];
        // Calculate statue decay tick length
        startTimer = _statueDecayOption.GetLengthSeconds() / _healthSystem.startingHealth;
        curTimer = startTimer;
    }

    private void Update()
    {
        if (_statueDecayOption.lengthMinutes > 0) // If lengthMinutes is -1, statue decay is disabled.
        {
            curTimer -= Time.deltaTime;
            
            if (curTimer <= 0f)
            {
                _healthSystem.TakeDamage(1, null);
                curTimer = startTimer;
            }
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
        foreach (HealthSystem healthSystem in owner)
        {
            healthSystem.ResetHealth();
        }
        
        // change sprite to crumbled statue
        // _spriteRenderer.sprite = destroyedSprite;
    }

    public bool StillThere()
    {
        return isStillThere;
    }
}
