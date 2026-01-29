using System;
using UnityEngine;

namespace UI.LevelSelect
{
    public class LevelSelectView : MonoBehaviour
    {
        public Action<string> OnLevelSelected;
        public MapSlotContainer mapSlotContainer;
        public string lobbySceneName;

        private void Awake()
        {
            //SelectLevel("3PlayerStatueNonSpecificGods1");
        }
        
        /// <summary>
        /// need to call this function to set which level will be loaded
        /// </summary>
        /// <param name="level"></param>
        /*public void SelectLevel(string level)
        {
            CurrentSelectedLevel = level;
        }*/

        public void StartLevel()
        {
            SFX.Instance.PlaySound("ui_select");
            OnLevelSelected?.Invoke(mapSlotContainer.currentSelectedScene);
        }

        public void BackToLobby()
        {
            SFX.Instance.PlaySound("ui_back");
            Utility.UnloadLevel("GameplayScene");
            Utility.LoadLevel("GameplayScene");
        }
    }
}