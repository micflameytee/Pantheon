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
            _abilityTimer = _abilityDuration;
            _playerSpriteRenderer.sprite = AbilitySprite;
            StartCooldown(_abilityCooldown);
        }

        public override int CalculateDealDamage(int inputDamage)
        {
            if (_abilityTimer > 0)
            {
                return inputDamage * _attackDamageMultiplier;
            }
            return base.CalculateDealDamage(inputDamage);
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