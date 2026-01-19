using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private List<PlayerController> _players;
    private List<Statue> _angels;

    public void StartGame(List<PlayerController> players)
    {
        _players = players;
        int i = 0;
        
        foreach (PlayerController player in _players)
        {
            //player.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            player.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
            _angels[i].owner = player;
            player.ownedStatue = _angels[i];
            player.ResetCooldown();
            i++;
        }
    }

    private void HandlePlayerDeath(PlayerController obj)
    {
        //
    }
}
