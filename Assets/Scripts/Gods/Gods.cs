using System;
using System.Collections;
using System.Collections.Generic;
using PlayerGods;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

public class Gods : MonoBehaviour
{
    [SerializeField] private List<PlayerClassBase> PlayerClasses;
    private PlayerClassBase _currentPlayerClass;
    private int _currentPlayerClassIndex;
    public PlayerClassBase CurrentPlayerClass => _currentPlayerClass;

    public PlayerController player;
    public HealthSystem healthSystem;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite stoneBody;
    public Sprite normalSprite;

    public event Action<godTypes> OnGodTypeChanged;
    public event Action<PlayerClassBase> OnPlayerClassChanged;
    
    public enum godTypes
    {
        None,
        Trickster,
        Defender,
        Speedster,
        SpellCaster
    }
    public godTypes godType;
    
    
    private float curSpecialCooldown = 0f;
    public float maxSpecialCooldown = 20f;

    private float curAbilityTime = 0f;
    public float maxAbilityTime = 5f;

    public Trickster trickster;
    public SpellCaster spellcaster;
    public Speedster speedster;

    private void Awake()
    {
        player = this.GetComponent<PlayerController>();
        healthSystem = this.GetComponent<HealthSystem>();
        SetPlayerClassIndex(0);
        //setType(godTypes.Trickster);
        //normalSprite = _spriteRenderer.sprite;
    }

    public void HandleSpecialAbility(InputAction.CallbackContext context)
    {
        if (curSpecialCooldown <= 0 && !player.GetGhost())
        {
            switch (godType)
            {
                case godTypes.Trickster:
                    TricksterAbility();
                    break;
                case godTypes.Defender:
                    DefenderAbility();
                    break;
                case godTypes.Speedster:
                    SpeedsterAbility();
                    break;
                case godTypes.SpellCaster:
                    SpellCasterAbility();
                    break;
                
            }
        }
        
        
        //  Command pattern
        _currentPlayerClass.PerformSpecialAbility();
    }

    private void SpellCasterAbility()
    {
        Debug.Log($"not implemented yet");
    }
    


    private void SpeedsterAbility()
    {
        Debug.Log($"not implemented yet");
    }

    
    
    
    private void DefenderAbility()
    {
        player.IsStone = true;
        healthSystem.IsStone = true;
        curAbilityTime = maxAbilityTime;
        _spriteRenderer.sprite = stoneBody;
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
        _currentPlayerClass.Tick();
        curSpecialCooldown -= Time.deltaTime;      
        curAbilityTime -= Time.deltaTime;
        if (godType == godTypes.Defender && player.IsStone && curAbilityTime <= 0)
        {
            player.IsStone = false;
            healthSystem.IsStone = false;
            _spriteRenderer.sprite = normalSprite;
        }
    }

    /// <summary>
    /// Trigger god change
    /// </summary>
    public void ChangeGod()
    {
        if (godType == godTypes.SpellCaster)
        {
            godType = godTypes.Trickster;
        }
        else
        {
            godType++;
        }
        OnGodTypeChanged?.Invoke(godType);


        // Command Pattern method
        _currentPlayerClassIndex++;
        SetPlayerClassIndex(_currentPlayerClassIndex);
    }

    private void SetPlayerClassIndex(int index)
    {
        if (index >= PlayerClasses.Count)
        {
            index = 0;
        }
        _currentPlayerClassIndex  = index;
        var playerClass = PlayerClasses[index];
        if (_currentPlayerClass != null)
        {
            Destroy(_currentPlayerClass);
        }
        _currentPlayerClass = Instantiate(playerClass);
        _currentPlayerClass.name = playerClass.name;
        _currentPlayerClass.PlayerController = player;
        healthSystem.PlayerClass = _currentPlayerClass;
        
        OnPlayerClassChanged?.Invoke(_currentPlayerClass);
    }
}
