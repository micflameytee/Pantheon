using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] public AudioClip deploySfx;
    private PlayerController _otherController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"healthPotion");

        if (other.CompareTag("Player"))
        {
            _otherController = other.GetComponent<PlayerController>();
            
            // Ignore if player is ghost 
            if (_otherController.GetGhost())
            {
                return;
            }
            
            // Health potion effect, destroy self  
            SFX.Instance.PlaySound(deploySfx, transform.position);
            _otherController.HealthSystem.ResetHealth();
            Destroy(this.gameObject);
        }
    }
}
