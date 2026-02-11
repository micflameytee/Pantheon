using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BackButtonController : MonoBehaviour
{
    
    
    GameInputActions inputActions;
    
    private void Awake()
    {
        inputActions = new GameInputActions();
        
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.MenuActions.MenuButton.started += HandleBackButton;
    }

    private void OnDisable()
    {
        inputActions.MenuActions.MenuButton.started -= HandleBackButton;
        inputActions.Disable();
    }

    public void HandleBackButton(InputAction.CallbackContext context)
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
