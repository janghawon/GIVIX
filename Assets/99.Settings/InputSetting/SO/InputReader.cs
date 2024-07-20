using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Setting/InputReader")]
public class InputReader : ScriptableObject, Controls.IPlayerActions
{
    private Controls _inputControl;

    [HideInInspector] public Vector3 MoveInput { get; private set; }
    [HideInInspector] public Vector2 MouseInput { get; private set; }
    [HideInInspector] public bool IsOnAttack { get; private set; }

    public event InputAction<EntityState> OnMovementEvent = null;
    public event InputAction<EntityState> OnAttackEvent = null;
    public event InputAction<EntityState> OnAttackEndEvent = null;
    public event InputAction<EntityState> OnRollEvent = null;

    public void OnMovement(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector3>();

        if(context.performed)
        {
            OnMovementEvent?.Invoke(EntityState.Run);
        }
    }

    public void OnRoll(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        OnRollEvent?.Invoke(EntityState.Roll);
    }

    public void OnAttack(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnAttackEvent?.Invoke(EntityState.Attack);
            IsOnAttack = true;
        }

        if(context.canceled)
        {
            OnAttackEndEvent?.Invoke(EntityState.Run);
            IsOnAttack = false;
        }
    }

    public void OnSkill(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        
    }

    public void OnMouse(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MouseInput = context.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        if(_inputControl is null)
        {
            _inputControl = new Controls();
            _inputControl.Player.SetCallbacks(this);
        }
        _inputControl.Player.Enable();
    }

    public delegate void InputAction();
    public delegate void InputAction<in T>(T value);
}
