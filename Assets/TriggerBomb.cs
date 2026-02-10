using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerBomb : MonoBehaviour
{
    [SerializeField] private float StartTime = 5f;
    [SerializeField] private SpriteAnimation explosionPrefab;
    [SerializeField] private SpriteAnimation bombAnimation;
    private RaycastHit2D[] _hits = new RaycastHit2D[20];
    public float radius = 1;
    private float tileSize = 0.25f;
    public PlayerController owner { get; set; }
    public int damage = 3;
    private int spriteCount;
    
    [SerializeField] private AudioClip deploySfx;
    [SerializeField] private AudioClip fuseSfx;


    private void Start()
    {
        SFX.Instance.PlaySound(fuseSfx, transform.position);
        bombAnimation.SetAnimationTime(StartTime);
        bombAnimation.OnAnimationEnd += Explode;
        bombAnimation.PlayAnimation();
        
    }


    private void Explode()
    {
        SFX.Instance.PlaySound(deploySfx, transform.position);
        SpriteAnimation explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.PlayAnimation();
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius * tileSize, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.GetComponent<HealthSystem>(); 
            targetSystem?.TakeDamage(damage, owner);
        }
    }

    public void SetInformation(float radius, float bombTime, int abilityDamage, PlayerController player)
    {
        owner = player;
        this.radius = radius;
        StartTime = bombTime;
        damage = abilityDamage;
    }
}
