using System;
using System.Collections;
using System.Collections.Generic;
using UI.LevelSelect;
using UI.Lobby;
using UI.PlayerHud;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalPlayerManager : MonoBehaviour
{
    public List<PlayerController> players = new ();
    private LobbyController _lobbyController;
    
    public PlayerInputManager playerInputManager;
    private LevelSelectController _levelSelectController;
    private LevelController _levelController;
    private GameOver _gameOverController;
    private bool _gameOver;
    private PlayerHudController _playerHudController;

    private void Awake()
    {
        _lobbyController = new LobbyController();
        _levelSelectController = new LevelSelectController();
        _playerHudController = new PlayerHudController();
        playerInputManager.DisableJoining();
    }

    private void Start()
    {
        StartCoroutine(LoadGameSequence());
    }

    private IEnumerator LoadGameSequence()
    {
        playerInputManager.DisableJoining();
        yield return _playerHudController.LoadHUD();
        playerInputManager.EnableJoining();
        yield return _lobbyController.LoadLobby();
        SetPlayerLobbyMode(false);
        playerInputManager.DisableJoining();
        yield return _levelSelectController.LoadLevelSelect();
        string selectedLevel = _levelSelectController.SelectedLevel;
        yield return LoadLevel(selectedLevel);
    }

    private IEnumerator LoadLevel(string selectedLevel)
    {
        Debug.Log($"Attempting to load Game Level {selectedLevel}");
        yield return SceneManager.LoadSceneAsync(selectedLevel, LoadSceneMode.Additive);
        //  tell the players that they can now play

        _levelController = GameObject.Find("LevelController").GetComponent<LevelController>();
        foreach (PlayerController player in players)
        {
            player.SetGhost(false);
        }
        SetPlayerLobbyMode(false);
        _levelController.StartGame(players);
        while (!_gameOver)
        {
            yield return null;
        }
        yield return SceneManager.UnloadSceneAsync(selectedLevel);
        yield return _playerHudController.UnloadHUD();
        
        // Not how you're meant to do this
        yield return SceneManager.LoadSceneAsync("GameOver", LoadSceneMode.Additive);
        _gameOverController = GameObject.Find("GameOver").GetComponent<GameOver>();
        _gameOverController.SetWinner(players[0].name);
    }

    public void OnPlayerAdded(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        Debug.Log($"MESSAGE: Player {pc?.name ?? "Null"} has been added", player.gameObject);
        pc.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
        players.Add(pc);
        pc.SetLobbyMode(true);
        pc.SetGhost(true);
        _lobbyController.AddNewPlayer(player);
        _playerHudController.AddPlayer(pc);
    }

    

    private void SetPlayerLobbyMode(bool isLobbyMode)
    {
        foreach (PlayerController player in players)
        {
            player.SetLobbyMode(isLobbyMode);
        }
    }

    private void HandlePlayerDeath(PlayerController player)
    {
        if (players.Contains(player))
        {
            if (player.OwnedStatue != null && !player.OwnedStatue.StillThere())
            {
                players.Remove(player);
            }
            player.SetGhost(true);
            
            if (players.Count < 2)
            {
                Debug.Log($"MESSAGE: Player {players[0].name} has won");
                _gameOver = true;
            }
        }
    }
}
