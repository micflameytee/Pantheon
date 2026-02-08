using UnityEngine;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Bomber", menuName = "Game/PlayerClass/Bomber")]
    public class PlayerClassBomber :  PlayerClassBase
    {
        [SerializeField] private float _cooldownTimer = 10f;
        [SerializeField] private int _abilityDamage = 5;
        [SerializeField] private float radius = 3f;
        [SerializeField] private float bombTime = 5f;
        

        [SerializeField] private TriggerBomb bomb;
        
        
        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            
            TriggerBomb currentBomb = Instantiate(bomb, PlayerController.transform.position, PlayerController.transform.rotation);

            currentBomb.SetInformation(radius, bombTime, _abilityDamage, PlayerController);
            
            StartCooldown(_cooldownTimer);
        }
    }
}