using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Lobby
{
    public class LobbyView : MonoBehaviour
    {
        public event Action OnStartGameRequested;
        [SerializeField] private TMP_Text _feedbackText;

        private void Awake()
        {
            SetFeedbackText("");
        }

        public void RequestStartGame()
        {
            OnStartGameRequested?.Invoke();
        }

        public void AddNewPlayer(PlayerInput player, string playerName)
        {
            SetFeedbackText($"{playerName} Joined the game");
        }

        private void SetFeedbackText(string feedbackText)
        {
            _feedbackText.text = feedbackText;
        }
    }
}