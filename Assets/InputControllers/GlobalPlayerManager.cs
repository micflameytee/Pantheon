using System;
using System.Collections;
using System.Collections.Generic;
using UI.LevelSelect;
using UI.Lobby;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobalPlayerManager : MonoBehaviour
{
    public List<PlayerController> players = new ();
    private LobbyController _lobbyController;
    
    public PlayerInputManager playerInputManager;
    private LevelSelectController _levelSelectController;
    private LevelController _leveController;
    private bool _gameOver;

    private void Awake()
    {
        _lobbyController = new LobbyController();
        _levelSelectController = new LevelSelectController();
    }

    private void Start()
    {
        StartCoroutine(LoadGameSequence());
    }

    private IEnumerator LoadGameSequence()
    {
        playerInputManager.EnableJoining();
        yield return _lobbyController.LoadLobby();
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

        _leveController = GameObject.Find("LevelController").GetComponent<LevelController>();
        foreach (PlayerController player in players)
        {
            player.SetGhost(false);
        }
        _leveController.StartGame(players);
        while (!_gameOver)
        {
            yield return null;
        }
        yield return SceneManager.UnloadSceneAsync(selectedLevel);
        
        SceneManager.LoadScene("MainMenu");
    }

    public void OnPlayerAdded(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        Debug.Log($"MESSAGE: Player {pc?.name ?? "Null"} has been added", player.gameObject);
        pc.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
        players.Add(pc);
        pc.SetGhost(true);
        _lobbyController.AddNewPlayer(player);
        
    }

    private void HandlePlayerDeath(PlayerController player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            player.SetGhost(true);
            // Destroy(player.gameObject);
            if (players.Count == 1)
            {
                Debug.Log($"MESSAGE: Player {players[0].name} has won");
                _gameOver = true;
            }
        }
    }
}
