using UnityEngine;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Atheist", menuName = "Game/PlayerClass/Atheist")]
    public class PlayerClassAtheist : PlayerClassBase
    {
        [SerializeField] private int _attackDamageMultiplier = 2;
        [SerializeField] private float _abilityCooldown = 10f;
        [SerializeField] private float _abilityDuration = 5f;
        private float _abilityTimer = 0f;
        private int _resetValue = 1;


        public override void Setup()
        {
            base.Setup();
        }

        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            _abilityTimer = _abilityDuration;
            StartCooldown(_abilityCooldown);
        }

        public override int CalculateDamage(int inputDamage)
        {
            if (_abilityTimer > 0)
            {
                return inputDamage * _attackDamageMultiplier;
            }
            return base.CalculateDamage(inputDamage);
        }

        public override void Tick()
        {
            _abilityTimer -= Time.deltaTime;
            base.Tick();
        }
    }
}