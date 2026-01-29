using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.Lobby
{
    public class LobbyController
    {
        private LobbyView _view;
        private bool _isGameStartRequested = false;
        private GameInputActions _inputActions;
        private int _playerCount;
        
        public IEnumerator LoadLobby()
        {
            Debug.Log($"Loading Lobby");
            yield return SceneManager.LoadSceneAsync("Lobby",  LoadSceneMode.Additive);
            _view = GameObject.FindWithTag("LobbyView").GetComponent<LobbyView>();
            
            _inputActions = new GameInputActions();
            _inputActions.MenuActions.Enable();
            _inputActions.MenuActions.NextScene.performed += HandleNextSceneRequested;
            _view.OnStartGameRequested += HandleOnStartGameRequested;
            while (!_isGameStartRequested)
            {
                yield return null;
            }
            
            _inputActions.MenuActions.NextScene.performed -= HandleNextSceneRequested;
            _inputActions.MenuActions.Disable();

            yield return UnloadLobby();
        }

        private void HandleNextSceneRequested(InputAction.CallbackContext obj)
        {
            HandleOnStartGameRequested();
        }

        public void HandleOnStartGameRequested()
        {
            _isGameStartRequested = true;
        }

        public IEnumerator UnloadLobby()
        {
            yield return SceneManager.UnloadSceneAsync("Lobby");
            _view = null;
            SFX.Instance.PlaySound("ui_select");
        }


        public void AddNewPlayer(PlayerInput player)
        {
            _playerCount++;
            _view?.AddNewPlayer(player, "Player " + _playerCount);
            SFX.Instance.PlaySound("ui_chime");
        }
    }
}