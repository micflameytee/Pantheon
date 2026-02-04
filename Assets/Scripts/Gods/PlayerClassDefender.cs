using UnityEngine;

namespace PlayerGods
{

    [CreateAssetMenu(fileName = "Defender", menuName = "Game/PlayerClass/Defender")]
    public class PlayerClassDefender : PlayerClassBase
    {
        [SerializeField] private float _shieldTime = 1f;
        [SerializeField] private float _shieldSpeedMultiplier = 0f;
        private float _abilityTimer;
        
        public override void PerformSpecialAbility()
        {
            _abilityTimer = _shieldTime;
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
        }
    }
}