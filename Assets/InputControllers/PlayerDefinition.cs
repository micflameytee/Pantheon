using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input Controller", fileName = "Player1")]    
public class PlayerDefinition : ScriptableObject
{
    public string PlayerName;
    public string PlayerClass;
    public InputActionAsset inputActions;
    
    public event Action<Vector2> OnMove;
    public event Action OnAttack;
}
