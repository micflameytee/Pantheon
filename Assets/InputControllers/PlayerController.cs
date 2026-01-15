using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public event Action<PlayerController> OnPlayerDeath;

    public float MoveSpeed = 10f;
    private Vector2 _moveDirection = Vector2.zero;
    
    public void HandleMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        
        transform.Translate( MoveSpeed * Time.deltaTime * _moveDirection);   
    }


    public void HandlePlayerAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking");
    }

}
