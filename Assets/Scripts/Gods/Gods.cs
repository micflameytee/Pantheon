using System;
using PlayerGods;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gods : MonoBehaviour
{
    [SerializeField] private PlayerList PlayerClasses;
    private PlayerClassBase _currentPlayerClass;
    private int _currentPlayerClassIndex;
    public PlayerClassBase CurrentPlayerClass => _currentPlayerClass;

    public PlayerController player;
    public HealthSystem healthSystem;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Sprite normalSprite;

    public event Action<PlayerClassBase> OnPlayerClassChanged;

    private int _godsEnabled;


    private void Awake()
    {
        _godsEnabled = PlayerPrefs.GetInt("GodsEnabled", 1);
        player = this.GetComponent<PlayerController>();
        healthSystem = this.GetComponent<HealthSystem>();
        SetPlayerClassIndex(0);
    }

    public void HandleSpecialAbility(InputAction.CallbackContext context)
    {
        if (player.GetGhost() || player.GetLobbyMode())
            return;
        //  Command pattern
        _currentPlayerClass.PerformSpecialAbility();
    }

    
    void Update()
    {
        _currentPlayerClass.Tick();
    }

    /// <summary>
    /// Trigger god change
    /// </summary>
    public void ChangeGod()
    {
        if (_godsEnabled == 0) { return; }
        // Command Pattern method
        _currentPlayerClassIndex++;
        SetPlayerClassIndex(_currentPlayerClassIndex);
    }

    private void SetPlayerClassIndex(int index)
    {
        if (index >= PlayerClasses.playerList.Count)
        {
            index = 0;
        }
        _currentPlayerClassIndex  = index;
        var playerClass = PlayerClasses.playerList[index];
        if (_currentPlayerClass != null)
        {
            Destroy(_currentPlayerClass);
        }
        _currentPlayerClass = Instantiate(playerClass);
        _currentPlayerClass.name = playerClass.name;
        _currentPlayerClass.PlayerController = player;
        healthSystem.PlayerClass = _currentPlayerClass;
        _currentPlayerClass.Setup();
        normalSprite = _currentPlayerClass.PlayerSprite;
        OnPlayerClassChanged?.Invoke(_currentPlayerClass);
    }

}
