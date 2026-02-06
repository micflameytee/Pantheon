using UnityEngine;

namespace PlayerGods
{
    [CreateAssetMenu(fileName = "No Class", menuName = "Game/PlayerClass/No Class")]
    

    public class PlayerClassBase : ScriptableObject
    {
        public Sprite PlayerSprite;
        [HideInInspector] public PlayerController PlayerController;
        private float _cooldown;
        protected bool IsOnCooldown => _cooldown > 0;
        
        public AudioClip abilitySfx;
        public AudioClip abilityReloadSfx;

        public virtual void PerformSpecialAbility()
        {
            
        }

        public virtual void Setup()
        {
            
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