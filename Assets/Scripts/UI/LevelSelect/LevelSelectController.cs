using System.Collections;
using UI.Lobby;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UI.LevelSelect
{
    public class LevelSelectController
    {
        private LevelSelectView _view;
        private bool _isGameStartRequested = false;
        private GameInputActions _inputActions;
        private int _playerCount;
        
        public string SelectedLevel { get; private set; }
        
        public IEnumerator LoadLevelSelect()
        {
            yield return SceneManager.LoadSceneAsync("LevelSelect",  LoadSceneMode.Additive);
            _view = GameObject.Find("LevelSelectView").GetComponent<LevelSelectView>();
            
            _view.OnLevelSelected += HandleLevelSelected;
            while (!_isGameStartRequested)
            {
                yield return null;
            }

            yield return UnloadLobby();
        }


        public void HandleLevelSelected(string level)
        {
            SelectedLevel = level;
            _isGameStartRequested = true;
        }

        public IEnumerator UnloadLobby()
        {
            yield return SceneManager.UnloadSceneAsync("LevelSelect");
            _view = null;
        }

    }
}