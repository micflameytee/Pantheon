using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 1;
    public float strikeCooldown = 0.25f;
    public float radius = 1;
    private float _cooldownTimer = 0f;
    private float baseCooldown;
    
    private RaycastHit2D[] _hits = new RaycastHit2D[20];
    private HealthSystem _myHealthSystem;
    private PlayerController _player;
    [SerializeField] private SwordAnimation _swordAnimation;


    public void Initialize(PlayerController player)
    {
        _player = player;
        _myHealthSystem = player.GetComponent<HealthSystem>();
        _swordAnimation.SwingTime = strikeCooldown;
        baseCooldown = strikeCooldown;
    }
    

    public int CalculateDamage()
    {
        return _player.God.CurrentPlayerClass.CalculateDamage(damage);
    }
    

    private void Update()
    {
        _cooldownTimer -= _player.God.CurrentPlayerClass.CalculateAttackSpeed(Time.deltaTime);
    }

    public void PreformAttack()
    {
        if (_cooldownTimer > 0)
        {
            return;
        }
        
        _swordAnimation.PerformAnimation();
        
//        Debug.Log(name + " is attacking");
        SFX.Instance.PlaySound(_player.damageSound, transform.position);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        int numHits = Physics2D.CircleCastNonAlloc(position, radius, Vector2.zero, _hits, 0f);
        for (int i = 0; i < numHits; i++)
        {
            RaycastHit2D hit = _hits[i];
            HealthSystem targetSystem = hit.collider.gameObject.GetComponent<HealthSystem>();
            if (targetSystem != null && _myHealthSystem != targetSystem)
            {
//                Debug.Log($"{_myHealthSystem.name} is attacking {targetSystem.name}");
                
                targetSystem.TakeDamage(CalculateDamage(), _player);
            }
        }

        _cooldownTimer = strikeCooldown;
    }
    
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}