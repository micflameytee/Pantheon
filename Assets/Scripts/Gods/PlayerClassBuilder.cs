using UnityEditor;
using UnityEngine;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Builder", menuName = "Game/PlayerClass/Builder")]
    public class PlayerClassBuilder : PlayerClassBase
    {
        [SerializeField] private int wallHealth = 5;
        [SerializeField] private PlaceableWalls WallSet;
        private PlaceableWalls _placedWalls;
        [SerializeField] private float cooldown = 10f;
        [SerializeField] private float tilesInfront = 2;
        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;

            float distance = 0.25f * tilesInfront;
            
            _placedWalls = Instantiate(WallSet, PlayerController.transform.position + PlayerController.transform.up * distance, PlayerController.transform.rotation);
            
            _placedWalls.WallHealth(wallHealth);
            
            
            StartCooldown(cooldown);
        }
    }
}