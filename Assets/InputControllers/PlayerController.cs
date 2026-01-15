using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameInputActions inputActions;

    public event Action<PlayerController> OnPlayerDeath;

    public float MoveSpeed = 10f;
    
    private Vector2 _moveDirection = Vector2.zero;
    public PlayerDefinition _playerDefinition;
    
    // Start is called before the first frame update
    void Start()
    {
        inputActions = new GameInputActions();
        inputActions.Enable();
        
        
        inputActions.GameActions.Move.started += HandleMove;
        inputActions.GameActions.Move.performed += HandleMove;
        inputActions.GameActions.Move.canceled += HandleMoveEnd;
        inputActions.GameActions.Attack.started += HandlePlayerAttack;
        
        _playerDefinition.OnMove += HandleMoveSO;
    }

    private void HandleMoveSO(Vector2 obj)
    {
    }

    private void OnDestroy()
    {
        inputActions.GameActions.Move.started -= HandleMove;
        inputActions.GameActions.Move.performed -= HandleMove;
        inputActions.GameActions.Move.canceled -= HandleMoveEnd;

        inputActions.GameActions.Attack.started -= HandlePlayerAttack;
    }
    
    private void HandleMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
    }
    private void HandleMoveEnd(InputAction.CallbackContext obj)
    {
        _moveDirection = Vector2.zero; 
    }

    private void Update()
    {
        transform.Translate( MoveSpeed * Time.deltaTime * _moveDirection);   
    }


    private void HandlePlayerAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attacking");
    }

}
