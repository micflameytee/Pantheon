using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private List<PlayerController> _players;
    [SerializeField] private List<Statue> _angels;

    public void StartGame(List<PlayerController> players)
    {
        _players = players;
        int i = 0;
        
        foreach (PlayerController player in _players)
        {
            //check that there is an angel for this player
            
            player.ResetCooldown();
            player.SetGhost(false);
            
            //player.transform.position = SpawnPoints[Random.Range(0, SpawnPoints.Length)].position;
            Statue angel = _angels[i];
            player.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
            angel.owner = player;
            player.ownedStatue = angel;
            
            player.transform.position = angel.GetSpawnPoint().position;
            
            
            // _currentSpawnPointIndex = (_currentSpawnPointIndex + 1) % spawnPoints.Length;
            i++;
        }
    }

    private void HandlePlayerDeath(PlayerController obj)
    {
        //
    }
}
