using UnityEngine;

namespace PlayerGods
{
    
    [CreateAssetMenu(fileName = "Speedster", menuName = "Game/PlayerClass/Speedster")]
    public class PlayerClassSpeedster : PlayerClassBase
    {
        private const float tileSize = 0.25f;
        private int wallLayerMask;
        [SerializeField] private float blinkDistance = 5f;
        [SerializeField] private float blinkCooldown = 4f;
        private float playerRadius;

        public override void Setup()
        {
            wallLayerMask = LayerMask.GetMask("OuterWall");
            playerRadius = PlayerController.GetComponent<CircleCollider2D>().radius;
        }
        
        public override void PerformSpecialAbility()
        {
            if (IsOnCooldown)
                return;
            Quaternion playerRotation = PlayerController.transform.rotation;
            float totalDistance = tileSize * blinkDistance;
            RaycastHit2D hit2d = Physics2D.CircleCast(
                PlayerController.transform.position, 
                playerRadius,
                playerRotation.eulerAngles, 
                totalDistance,
                wallLayerMask);
            if (hit2d.collider != null)
            {
                PlayerController.transform.position = hit2d.point;
            }
            else
            {
                PlayerController.transform.position =
                    PlayerController.transform.position +
                    playerRotation * Vector2.up * totalDistance;
            }
            StartCooldown(blinkCooldown);
        }
    }
}