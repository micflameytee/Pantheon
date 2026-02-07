#nullable enable
using System;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Trickster", menuName = "Game/PlayerClass/Trickster")]
    public class PlayerClassTrickster : PlayerClassBase
    {
        
        [SerializeField] private Recall recallPrefab;
        private Recall? _recall;
        [SerializeField] private float _placementCooldown = 10f;
        [SerializeField] private float _recallCooldown = 1f;
        [SerializeField] private float trapCooldownMultiplier = 1f;
        private float resetValue = 1f;

        private void Awake()
        {
        }

        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            
            if (_recall != null)
            {
                PlayerController.HandleTrapCooldownMultiplier(resetValue);
                PlayerController.transform.position = _recall.transform.position;
                _recall.SetExpired(true);
                _recall = null;
                StartCooldown(_placementCooldown);
            }
            else
            {
            PlayerController.HandleTrapCooldownMultiplier(trapCooldownMultiplier);
                _recall = Instantiate(recallPrefab, PlayerController.transform.position, PlayerController.transform.rotation);
                _recall.SetExpired(false);
                StartCooldown(_recallCooldown);
            }
        }
    }
}