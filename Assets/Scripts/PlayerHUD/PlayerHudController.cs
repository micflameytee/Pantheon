using System.Collections;
using System.Collections.Generic;
using UI.Lobby;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.PlayerHud
{
    public class PlayerHudController
    {
        private PlayerHudView _view;
        

        public IEnumerator LoadHUD()
        {
            // Debug.Log($"Loading PlayerHud");
            yield return SceneManager.LoadSceneAsync("PlayerHUD",  LoadSceneMode.Additive);
            _view = GameObject.FindObjectOfType<PlayerHudView>();
        }

        public IEnumerator UnloadHUD()
        {
            yield return SceneManager.UnloadSceneAsync("PlayerHUD");
        }

        public void AddPlayer(PlayerController player)
        {
            
            _view.AddPlayer(player);
        }

        public void RemovePlayer(PlayerController player)
        {
            _view.RemovePlayer(player);
        }
    }
}