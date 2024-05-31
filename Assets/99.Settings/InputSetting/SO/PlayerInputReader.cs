using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/PlayerInputReader")]
public class PlayerInputReader : InputReader, InputControl.IPlayerActions
{
    public event InputEventListener OnAttackEvent = null;
    public event InputEventListener OnAttackEndEvent = null;
    public event InputEventListener OnRollEvent = null;
    public event InputEventListener<bool> OnShieldEvent = null;

    private bool _isOnAttackRun;
    public event InputEventListener OnAttackRunEvent = null;
    public event InputEventListener OnAttackRunEndEvent = null;
    
    [HideInInspector] public Vector3 movementInput;
    [HideInInspector] public Vector2 screenPos;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        InputControl.Player.Enable();
    }

    protected override void CreateNewInputAsset()
    {
        base.CreateNewInputAsset();
        InputControl.Player.SetCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementInput.z = movementInput.y;
        movementInput.y = 0;
    }

    public void OnScreenPos(InputAction.CallbackContext context)
    {
        screenPos = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (movementInput.sqrMagnitude > 0.03f)
        {
            _isOnAttackRun = true;
            OnAttackRunEvent?.Invoke();
        }

        if (movementInput.sqrMagnitude <= 0.03f && _isOnAttackRun)
        {
            OnAttackRunEndEvent?.Invoke();
        }

        if (context.started)
        {
            OnAttackEvent?.Invoke();
        }

        if(context.canceled)
        {
            OnAttackEndEvent?.Invoke();
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnRollEvent?.Invoke();
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnShieldEvent?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnShieldEvent?.Invoke(false);
        }
    }
}
