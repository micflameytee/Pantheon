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

    public event Action<PlayerClassBase> OnPlayerClassChanged;
    
    
    private float curSpecialCooldown = 0f;
    public float maxSpecialCooldown = 20f;

    private float curAbilityTime = 0f;
    public float maxAbilityTime = 5f;

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
        
        
        
        //  Command pattern
        _currentPlayerClass.PerformSpecialAbility();
    }

    
    void Update()
    {
        _currentPlayerClass.Tick();
        curSpecialCooldown -= Time.deltaTime;
    }

    /// <summary>
    /// Trigger god change
    /// </summary>
    public void ChangeGod()
    {
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
        _currentPlayerClass.Setup();
        
        OnPlayerClassChanged?.Invoke(_currentPlayerClass);
    }
}
