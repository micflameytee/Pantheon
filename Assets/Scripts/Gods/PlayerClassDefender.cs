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
        [SerializeField] private float attackSpeedMultiplier = 1;
        [SerializeField] private float trapCooldownMultiplier = 1f;

        
        private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private Sprite AbilitySprite;


        public override void Setup()
        {
            _playerSpriteRenderer = PlayerController.GetComponent<SpriteRenderer>();
        }

        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            
            _abilityTimer = _shieldTime;
            StartCooldown(_shieldCooldown);
            _playerSpriteRenderer.sprite = AbilitySprite;

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

        public override int CalculateTakeDamage(int inputDamage)
        {
            if (_abilityTimer > 0)
            {
                return  0;
            }

            return inputDamage;
        }

        public override float CalculateTrapCooldown(float inputCooldown)
        {
            
            float baseSpeed = base.CalculateTrapCooldown(inputCooldown);

            if (_abilityTimer > 0)
            {
                return baseSpeed *  trapCooldownMultiplier;
            }
            return baseSpeed;
        }


        public override float CalculateAttackSpeed(float inputSpeed)
        {
            float baseAttackSpeed = base.CalculateAttackSpeed(inputSpeed);
            if (_abilityTimer > 0)
            {
                return  baseAttackSpeed * attackSpeedMultiplier;
            }
            return baseAttackSpeed;
        }

        public override void Tick()
        {
            _abilityTimer -= Time.deltaTime;
            if(_abilityTimer < 0 && _playerSpriteRenderer.sprite == AbilitySprite)
            {
                _playerSpriteRenderer.sprite = PlayerSprite;
            }
            base.Tick();
        }
    }
}