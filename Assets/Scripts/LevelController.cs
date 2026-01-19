using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Transform[] SpawnPoints;
    private List<PlayerController> _players;

    public void StartGame(List<PlayerController> players)
    {
        _players = players;
        foreach (PlayerController player in _players)
        {
            //player.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            player.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath(PlayerController obj)
    {
        //
    }
}
