using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerGods
{

    [CreateAssetMenu(fileName = "Defender", menuName = "Game/PlayerClass/Defender")]
    public class PlayerClassDefender : PlayerClassBase
    {
        [SerializeField] private float _shieldTime = 3f;
        [SerializeField] private float _shieldSpeedMultiplier = 0f;
        private float _abilityTimer;
        [SerializeField] private float _shieldCooldown = 10f;
        
        
        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            
            _abilityTimer = _shieldTime;
            StartCooldown(_shieldCooldown);
        }

        public override float SpeedMultiplier
        {
            get
            {
                if (_abilityTimer > 0)
                {
                    return  _shieldSpeedMultiplier;
                }

                return 1f;
            }
        }

        public override int CalculateDamage(int inputDamage)
        {
            if (_abilityTimer > 0)
            {
                return  0;
            }

            return inputDamage;
        }

        
        public override void Tick()
        {
            _abilityTimer -= Time.deltaTime;
            base.Tick();
        }
    }
}