using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalPlayerManager : MonoBehaviour
{
    private List<PlayerController> _players = new ();

    public void OnPlayerAdded(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        
        pc.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
        _players.Add(pc);
    }

    private void HandlePlayerDeath(PlayerController player)
    {
        if (_players.Contains(player))
        {
            _players.Remove(player);
            player.SetGhost();
            // Destroy(player.gameObject);
            if (_players.Count == 1)
            {
                Debug.Log($"MESSAGE: Player {_players[0].name} has won");
            }
        }
    }
}
