using System.Collections.Generic;
using UnityEngine;

namespace PlayerGods
{
    [CreateAssetMenu(fileName = "No Class", menuName = "Game/PlayerClass/No Class")]
    

    public class PlayerClassBase : ScriptableObject
    {
        public Sprite PlayerSprite;
        [HideInInspector] public PlayerController PlayerController;
        private float _cooldown;
        private Statue _playerStatue;
        [SerializeField] private List<Sprite> _statueSprites;
        private SpriteRenderer _statueRenderer;
        protected bool IsOnCooldown => _cooldown > 0;
        
        public AudioClip abilitySfx;
        public AudioClip abilityReloadSfx;

        public virtual void PerformSpecialAbility()
        {
            
        }

        public virtual void Setup()
        {
        }

        public virtual void levelStart()
        {
            if (_statueSprites.Count > 1)
            {
                _playerStatue = PlayerController.OwnedStatue;
                _statueRenderer = _playerStatue.GetComponent<SpriteRenderer>();
                _statueRenderer.sprite = _statueSprites[0];
                _playerStatue.GetComponent<HealthSystem>().OverrideSprites(_statueSprites);
            }
        }
        
        public virtual int CalculateDamage(int inputDamage) => inputDamage;
        
        public virtual float SpeedMultiplier => 1f;

        public virtual void Tick()
        {
            _cooldown -= Time.deltaTime;
        }

        protected void StartCooldown(float cooldownLength)
        {
            _cooldown = cooldownLength;
            
            if (abilitySfx != null)
            {
                SFX.Instance.PlaySound(abilitySfx, PlayerController.transform.position);
            }
            PlayerController.HandleAbility(_cooldown, abilityReloadSfx);
        }
    }
    
    
    
}