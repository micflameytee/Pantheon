using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gods : MonoBehaviour
{
    
    public PlayerController player;
    public HealthSystem healthSystem;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite stoneBody;
    
    public enum godTypes
    {
        Trickster,
        Defender,
        Speedster,
        SpellCaster
    }
    public godTypes? godType;
    
    
    private float curSpecialCooldown = 0f;
    public float maxSpecialCooldown = 20f;

    public Trickster trickster;
    public SpellCaster spellcaster;
    public Speedster speedster;
    public Defender defender;

    private void Awake()
    {
        player = this.GetComponent<PlayerController>();
        setType(godTypes.Trickster);
    }

    public void HandleSpecialAbility(InputAction.CallbackContext context)
    {
        if(godType.HasValue && curSpecialCooldown <= 0)
        {
        
            if (godType == godTypes.Trickster)
            {
                TricksterAbility();
            }

            if (godType == godTypes.Defender)
            {
                DefenderAbility();
            }

            if (godType == godTypes.Speedster)
            {
                SpeedsterAbility();
            }

            if (godType == godTypes.SpellCaster)
            {
                SpellCasterAbility();
            }
            
        }
    }

    private void SpellCasterAbility()
    {
        throw new NotImplementedException();
    }
    


    private void SpeedsterAbility()
    {
        throw new NotImplementedException();
    }

    
    
    
    private void DefenderAbility()
    {
        player.IsStone = true;
    }

    
    
    
    private void TricksterAbility()
    {
        if (curSpecialCooldown > 0 || player.GetGhost())
        {
            Debug.Log($"{curSpecialCooldown} seconds left");
            Debug.Log($"ghost is {player.GetGhost()}");
            return;
        }
        curSpecialCooldown = maxSpecialCooldown;
        Debug.Log($"{godType}");
        var newInstance = Instantiate(trickster, transform.position, transform.rotation);
        newInstance.owner = this;
    }


    private void setType(godTypes type)
    {
        godType =  type;
    }
    
    
    
    
    void Update()
    {
        curSpecialCooldown -= Time.deltaTime;        
    }
}
