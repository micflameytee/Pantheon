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
        public Slider StatueHealth;
        private PlayerController _player;
        private Statue _statue;

        public void SetPlayer(string playerName, PlayerController player)
        {
            _player = player;
            _statue = _player.OwnedStatue;
            PlayerName.text = playerName;
            PlayerClass.text = $"Player {PlayerName}";
            player.God.OnPlayerClassChanged += UpdatePlayerClass;
        }

        private void UpdatePlayerClass(PlayerClassBase playerClass)
        {
            UpdatePlayerClassText();
        }

        public void RegisterStatue(Statue statue)
        {
            _statue = statue;
            HealthSystem statueHealthSystem = _statue.GetComponent<HealthSystem>();
            statueHealthSystem.OnDamaged += UpdateHealth;
            UpdateHealth(statueHealthSystem);  
        }


        private void UpdatePlayerClassText()
        {
            PlayerClass.text = $"{_player.God.CurrentPlayerClass?.name}";
        }

        private void UpdateHealth(HealthSystem statueHealth)
        {
            int statueStartHealth = statueHealth.startingHealth;
            int statueCurHealth = statueHealth.currentHealth;
            StatueHealth.maxValue = statueStartHealth;
            StatueHealth.value = Mathf.Clamp(statueCurHealth, 0,  statueStartHealth + 1);
        }
    }
}