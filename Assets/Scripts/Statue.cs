using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite UnDamagedSprite;
    [SerializeField] private Sprite DamagedSprite;
    [SerializeField] private Transform[] spawnPoints;
    
    public PlayerController owner { get; set; }
    public bool isStillThere = true;
    
    private int _currentSpawnPointIndex = 0;

    private void Awake()
    {
        _spriteRenderer.sprite = UnDamagedSprite;
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
        // change sprite to crumbled statue
        //_spriteRenderer.sprite = DamagedSprite;
        _spriteRenderer.color = new  Color32(200, 200, 0, 255);
    }

    public bool StillThere()
    {
        return isStillThere;
    }
}
