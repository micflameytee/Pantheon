using System;
using System.Collections.Generic;
using UnityEngine;
namespace UI.PlayerHud
{
    public class PlayerHudView :MonoBehaviour
    {
        [SerializeField] private PlayerInfo[] _playerInfoPanels;
        private Dictionary<PlayerController, PlayerInfo> _players = new();

        private void Awake()
        {
            foreach (var playerInfoPanel in _playerInfoPanels)
            {
                playerInfoPanel.gameObject.SetActive(false);
            }
        }

        public void AddPlayer(PlayerController player)
        {
            int nextPlayerNum = _players.Count;
            if (nextPlayerNum < _playerInfoPanels.Length)
            {
                PlayerInfo playerInfoPanel = _playerInfoPanels[nextPlayerNum];
                playerInfoPanel.SetPlayer($"Player {nextPlayerNum + 1}", player);
                
                player.RegisterInfoPanel(playerInfoPanel);
                
                _players.Add(player, playerInfoPanel);
                playerInfoPanel.gameObject.SetActive(true);
            }
            else
            {
                throw new InvalidOperationException("Unable to add more players than PlayerInfoPanels");
            }
        }

        public void RemovePlayer(PlayerController player)
        {
            if (_players.TryGetValue(player, out PlayerInfo playerInfoPanel))
            {
                playerInfoPanel.gameObject.SetActive(false);
                _players.Remove(player);
            }
            //  get the player panel   
        }
    }
}