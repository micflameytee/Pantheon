using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelController : MonoBehaviour
{
    private List<PlayerController> _players;
    [SerializeField] private List<Statue> angels;
    public Bomb bomb;
    public BearTrap bearTrap;
    [SerializeField] private Transform[] trapSpawns;

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
            Statue angel = angels[i];
            player.HealthSystem.OnPlayerDeath += HandlePlayerDeath;
            angel.owner = player;
            player.OwnedStatue = angel;
            
            player.transform.position = angel.GetSpawnPoint().position;
            
            
            i++;
        }

        foreach (Transform trapSpawn in trapSpawns)
        {
            int randomChance = Random.Range(1, 100);
            if (randomChance <= 5)
            {
                var newInstance = Instantiate(bomb, trapSpawn);
            }
            
            if (randomChance >= 95)
            {
                var newInstance = Instantiate(bearTrap, trapSpawn);
            }
        }
    }

    private void HandlePlayerDeath(PlayerController obj)
    {
        
    }
}
