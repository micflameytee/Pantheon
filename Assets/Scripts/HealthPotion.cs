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
        _otherController = other.GetComponent<PlayerController>();
        
        // Ignore if player is ghost 
        if (_otherController.GetGhost())
        {
            return;
        }
        
        if (_otherController != null && other.CompareTag("Player"))
        {
            // Health potion effect, destroy self  
            SFX.Instance.PlaySound(deploySfx, transform.position);
            _otherController.HealthSystem.ResetHealth();
            Destroy(this.gameObject);
        }
    }
}
