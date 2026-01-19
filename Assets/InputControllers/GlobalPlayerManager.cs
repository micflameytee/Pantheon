using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobalPlayerManager : MonoBehaviour
{
    public List<PlayerController> players = new ();

    public void OnPlayerAdded(PlayerInput player)
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        
        pc.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
        players.Add(pc);
    }

    private void HandlePlayerDeath(PlayerController player)
    {
        if (players.Contains(player))
        {
            players.Remove(player);
            player.SetGhost();
            // Destroy(player.gameObject);
            if (players.Count == 1)
            {
                Debug.Log($"MESSAGE: Player {players[0].name} has won");
            }
        }
    }
}
