using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Gambler", menuName = "Game/PlayerClass/Gambler")]
    public class PlayerClassGambler : PlayerClassBase
    {
        [SerializeField] private int _attackDamageMultiplier = 2;
        [SerializeField] private float _attackSpeedMultiplier = 2f;
        [SerializeField] private float _trapCooldownMultiplier = 2f;
        [SerializeField] private float _jackpotDuration = 5f;
        [SerializeField] private float _speedMultiplier = 1.5f;


        [SerializeField] private SpriteAnimation explosionPrefab;
        [SerializeField] private float radius = 4;
        [SerializeField] private int damage = 2;
        private RaycastHit2D[] _hits = new RaycastHit2D[30];
        private float tileSize = 0.25f;

        [SerializeField] private int damageTaken = 1;
        [SerializeField] private int explosionDamage = 5;
        [SerializeField] private float _secretJackpotTimer = 251f;
        private bool _secretJackpot = false;
        [SerializeField] private float _secretCooldownMultiplier = 10f;
        [SerializeField] private float _secretAttackMultiplier = 3f;
        [SerializeField] private int _secretDamageMultiplier = 3;
        [SerializeField] private float _secretSpeedMultiplier = 3f;
        
        [SerializeField] private PokeMachine _pokeMachine;

        [SerializeField] private float _abilityTimer = 0f;
        private float _abilityCooldown = 10f;
        private HealthSystem _healthSystem;

        [SerializeField] private AudioClip[] pokeSounds;

        public override void Setup()
        {
            _healthSystem = PlayerController.HealthSystem;
        }

        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown && !_secretJackpot)
                return;
            
            StartCooldown(1f);
            
            float randomChance = Random.Range(0f, 1f);

            PokeMachine pokeMachine = Instantiate(_pokeMachine, PlayerController.transform.position,  Quaternion.identity);
            

            Debug.Log($"Jackpot: {randomChance}");
            
            if (randomChance <= 0.10)
            {
                if (!_secretJackpot)
                    _abilityTimer = 0f;
                pokeMachine.EffectSpriteChange(1);
                Explode();
                PlaySound(pokeSounds[0]);
            }
            else if (randomChance <= 0.20)
            {
                if (!_secretJackpot)
                    _abilityTimer = 0f;
                pokeMachine.EffectSpriteChange(2);
                _healthSystem.TakeDamage(explosionDamage, null);
                PlaySound(pokeSounds[1]);
            }
            else if (randomChance <= 0.30)
            {
                if (!_secretJackpot)
                    _abilityTimer = 0f;
                pokeMachine.EffectSpriteChange(3);
                _healthSystem.TakeDamage(damageTaken, null);
                PlaySound(pokeSounds[2]);
            }
            else if (randomChance <= 0.80)
            {
                pokeMachine.ChangeSprite();
                PlaySound(pokeSounds[3]);
            }
            else if (randomChance <= 0.90)
            {
                pokeMachine.EffectSpriteChange(4);
                _healthSystem.ResetHealth();
                PlaySound(pokeSounds[4]);
            }
            else if (randomChance <= 0.9999)
            {
                pokeMachine.EffectSpriteChange(5);
                _abilityTimer = _jackpotDuration;
                PlaySound(pokeSounds[5]);
            }
            else
            {
                _secretJackpot = true;
                _abilityTimer = _secretJackpotTimer;
                pokeMachine.BigWin();
                PlaySound(pokeSounds[6]);
            }
            
        }

        public void Explode()
        {
            
            SpriteAnimation explosion = Instantiate(explosionPrefab, PlayerController.transform.position, PlayerController.transform.rotation);
            explosion.transform.localScale = new Vector3(radius / 2, radius / 2, radius / 2);
            explosion.PlayAnimation();
            Vector2 position = new Vector2(PlayerController.transform.position.x, PlayerController.transform.position.y);
            int numHits = Physics2D.CircleCastNonAlloc(position, radius * tileSize, Vector2.zero, _hits, 0f);
            for (int i = 0; i < numHits; i++)
            {
                RaycastHit2D hit = _hits[i];
                HealthSystem targetSystem = hit.collider.GetComponent<HealthSystem>(); 
                targetSystem?.TakeDamage(explosionDamage, null);
            }
        }

        public override void Tick()
        {
            _abilityTimer -= Time.deltaTime;
            if (_secretJackpot && _abilityTimer < 0)
                _secretJackpot = false;
            base.Tick();
        }
        
        
        public override float SpeedMultiplier
        {
            get
            {
                if (_secretJackpot)
                {
                    return _secretSpeedMultiplier;
                }
                
                if (_abilityTimer > 0)
                {
                    return _speedMultiplier;
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

        public override int CalculateDealDamage(int inputDamage)
        {
            if (_secretJackpot)
            {
                return inputDamage * _secretDamageMultiplier;
            }
            if (_abilityTimer > 0)
            {
                return inputDamage * _attackDamageMultiplier;
            }
            return base.CalculateDealDamage(inputDamage);
        }

        public override float CalculateTrapCooldown(float inputCooldown)
        {
            
            float baseSpeed = base.CalculateTrapCooldown(inputCooldown);

            if (_secretJackpot)
            {
                return baseSpeed * _secretCooldownMultiplier;
            }
            if (_abilityTimer > 0)
            {
                return baseSpeed *  _trapCooldownMultiplier;
            }
            return baseSpeed;
        }


        public override float CalculateAttackSpeed(float inputSpeed)
        {
            float baseAttackSpeed = base.CalculateAttackSpeed(inputSpeed);

            if (_secretJackpot)
            {
                return baseAttackSpeed * _secretAttackMultiplier;
            }
            if (_abilityTimer > 0)
            {
                return  baseAttackSpeed * _attackSpeedMultiplier;
            }
            return baseAttackSpeed;
        }

        private void PlaySound(AudioClip sound)
        {
            if (sound != null && SFX.Instance != null)
            {
                SFX.Instance.PlayDelayedSound(sound, abilitySfx.length, PlayerController.gameObject.transform.position);
            }
        }
        
    }
}