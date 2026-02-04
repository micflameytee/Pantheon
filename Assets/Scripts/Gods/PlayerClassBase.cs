using UnityEngine;

namespace PlayerGods
{
    [CreateAssetMenu(fileName = "No Class", menuName = "Game/PlayerClass/No Class")]
    

    public class PlayerClassBase : ScriptableObject
    {
        public Sprite PlayerSprite;
        public PlayerController PlayerController;

        public virtual void PerformSpecialAbility()
        {
        }

        public virtual int CalculateDamage(int inputDamage) => inputDamage;
        
        public virtual float SpeedMultiplier => 1f;

        public virtual void Tick()
        {
        }
    }
    
    
    
}