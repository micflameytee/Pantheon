using PlayerGods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlayerHud
{
    public class PlayerInfo :MonoBehaviour
    {
        public TMP_Text PlayerName;
        public TMP_Text PlayerClass;
        public Slider PlayerHealth;
        private PlayerController _player;

        public void SetPlayer(string playerName, PlayerController player)
        {
            _player = player;
            PlayerName.text = playerName;
            PlayerClass.text = $"Player {PlayerName}";
            player.HealthSystem.OnPlayerDamaged += UpdateHealth;
            player.God.OnPlayerClassChanged += UpdatePlayerClass;
            UpdateHealth(player);  
        }

        private void UpdatePlayerClass(PlayerClassBase playerClass)
        {
            UpdatePlayerClassText();
        }


        private void UpdatePlayerClassText()
        {
            PlayerClass.text = $"{_player.God.CurrentPlayerClass?.name}";
        }

        private void UpdateHealth(PlayerController player)
        {
            PlayerHealth.maxValue = player.HealthSystem.startingHealth;
            PlayerHealth.value = Mathf.Clamp(player.HealthSystem.currentHealth, 0,  player.HealthSystem.startingHealth);
        }
    }
}