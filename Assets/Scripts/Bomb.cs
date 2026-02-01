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
    [SerializeField] private SpriteAnimation _spriteAnimation;
    [SerializeField] private Sprite destroyedBomb;

    private void Awake()
    {
        ResizeExplosion();
    }

    private void ResizeExplosion()
    {
        float thisRadius = transform.localScale.magnitude;
        ExplosionSprite.transform.localScale = 2f * radius / thisRadius * Vector3.one;
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
        _spriteAnimation.PlayAnimation();
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.GetComponent<HealthSystem>(); 
            targetSystem?.TakeDamage(damage, owner);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
        //Gizmos.DrawWireSphere(transform.position, radius);
    }
}
