using Unity.VisualScripting;
using UnityEngine;

namespace PlayerGods
{
    [CreateAssetMenu(fileName = "SpellCaster", menuName = "Game/PlayerClass/SpellCaster")]
    public class PlayerClassSpellCaster :  PlayerClassBase
    {
        [SerializeField] private FireBall _fireBall;
        [SerializeField] private float _spellCooldown;
        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            
            var newInstance = Instantiate(_fireBall, PlayerController.transform.position, PlayerController.transform.rotation);
            newInstance.owner = PlayerController;
            newInstance.direction = PlayerController.transform.up;
            
            StartCooldown(_spellCooldown);
        }
    }
}