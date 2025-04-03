using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : SimpleSingleton<InputManager>
{
    InputActions inputActions;

    [HideInInspector]
    public Vector2 inputValues;

    protected override void Awake()
    {
        base.Awake();
        inputActions = new InputActions();
    }

    private void Start()
    {
        inputValues = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Snake.Move.started += ctx => OnMoveStarted(ctx);
        inputActions.Snake.Move.performed += ctx => OnMovePerformed(ctx);
        inputActions.Snake.Move.canceled += ctx => OnMoveCanceled(ctx);
    }

    private void OnDisable()
    {
        inputActions.Snake.Move.started -= ctx => OnMoveStarted(ctx);
        inputActions.Snake.Move.performed -= ctx => OnMovePerformed(ctx);
        inputActions.Snake.Move.canceled -= ctx => OnMoveCanceled(ctx);
        inputActions.Disable();
    }

    void OnMoveStarted(CallbackContext context)
    {

    }

    void OnMovePerformed(CallbackContext context)
    {
        inputValues = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(CallbackContext context)
    {
        inputValues = Vector2.zero;
    }
}
