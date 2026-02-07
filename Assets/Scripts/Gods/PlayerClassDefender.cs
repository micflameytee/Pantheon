using System;
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
        private DamageSystem _damageSystem;
        [SerializeField] private float attackSpeedMultiplier = 1;
        [SerializeField] private float trapCooldownMultiplier = 1f;
        private float resetValue = 1f;

        

        public override void PerformSpecialAbility()
        {
            _damageSystem = PlayerController.GetComponent<DamageSystem>();
            if (IsOnCooldown)
                return;
            
            _abilityTimer = _shieldTime;
            StartCooldown(_shieldCooldown);
            _damageSystem.HandleAttackSpeedMultiplier(attackSpeedMultiplier);
            PlayerController.HandleTrapCooldownMultiplier(trapCooldownMultiplier);

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
            if (_abilityTimer <= 0 && _damageSystem != null)
            {
                _damageSystem.HandleAttackSpeedMultiplier(resetValue);
                PlayerController.HandleTrapCooldownMultiplier(resetValue);

            }
        }
    }
}