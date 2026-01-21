using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage = 1;
    public float StrikeCooldown = 1f;
    public float radius = 1;
    private float _cooldownTimer = 0f;
    
    private RaycastHit2D[] _hits = new RaycastHit2D[20];
    private HealthSystem _myHealthSystem;
    private PlayerController _player;


    public void Initialize(PlayerController player)
    {
        _player = player;
        _myHealthSystem = player.GetComponent<HealthSystem>();
    }
    

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;
    }

    public void PreformAttack()
    {
        if (_cooldownTimer > 0)
        {
            return;
        }
        
        
        Debug.Log(name + " is attacking");
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.gameObject.GetComponent<HealthSystem>();
            if (targetSystem != null && _myHealthSystem != targetSystem)
            {
                Debug.Log($"{_myHealthSystem.name} is attacking {targetSystem.name}");
                
                targetSystem.TakeDamage(Damage, _player);
            }
        }

        _cooldownTimer = StrikeCooldown;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}