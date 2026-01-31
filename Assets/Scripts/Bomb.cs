using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bomb : MonoBehaviour
{   
    public PlayerController owner { get; set; }
    public int damage = 3;    
    private RaycastHit2D[] _hits = new RaycastHit2D[20];
    private PlayerController _otherController;
    public float radius = 1;
    [SerializeField] private SpriteRenderer BombSprite;
    [SerializeField] private SpriteRenderer ExplosionSprite;
    [SerializeField] private Sprite destroyedBomb;
    private float _explosionTimer = 0f;

    private void Awake()
    {
        ResizeExplosion();
    }

    private void ResizeExplosion()
    {
        float thisRadius = transform.localScale.magnitude;
        ExplosionSprite.transform.localScale = 2f * radius / thisRadius * Vector3.one;
        ExplosionSprite.enabled =false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"bomb");
        _otherController = other.GetComponent<PlayerController>();
        if (_otherController.GetGhost())
        {
            Debug.Log($"dead");
            return;
        }
        if (_otherController != null && other.CompareTag("Player") && owner != _otherController)
        {
            Debug.Log($"other player");
            Explode();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Explode()
    {
        BombSprite.sprite = destroyedBomb;
        _explosionTimer = 2f;
        ExplosionSprite.enabled = true;
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.GetComponent<HealthSystem>(); 
            targetSystem?.TakeDamage(damage, owner);
        }
    }

    private void Update()
    {
        _explosionTimer -=  Time.deltaTime;
        if (_explosionTimer < 0f && ExplosionSprite.gameObject.activeSelf)
        {
            ExplosionSprite.enabled = false;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}
