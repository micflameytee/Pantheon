using System;
using UnityEngine;

namespace UI.LevelSelect
{
    public class LevelSelectView : MonoBehaviour
    {
        public Action<string> OnLevelSelected;
        public string CurrentSelectedLevel;

        private void Awake()
        {
            SelectLevel("3PlayerStatueNonSpecificGods1");
        }

        
        /// <summary>
        /// need to call this function to set which level will be loaded
        /// </summary>
        /// <param name="level"></param>
        public void SelectLevel(string level)
        {
            CurrentSelectedLevel = level;
        }

        public void StartLevel()
        {
            OnLevelSelected?.Invoke(CurrentSelectedLevel);
        }
    }
}