using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputController : MonoBehaviour
{
    InputActions inputActions;

    [SerializeField]
    float moveSpeed, rotSpeed;

    float moveInput, rotInput;

    void Awake()
    {
        inputActions = new InputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Snake.Move.performed += ctx => OnMovePerformed(ctx);
        inputActions.Snake.Move.canceled += ctx => OnMoveCanceled(ctx);
    }

    private void OnDisable()
    {
        inputActions.Snake.Move.performed -= ctx => OnMovePerformed(ctx);
        inputActions.Disable();
    }

    // Remove after prototyping body links
    public float bodySpeed;
    private void FixedUpdate()
    {
        transform.rotation *= Quaternion.AngleAxis(rotInput * rotSpeed, transform.up);
        //transform.Translate(translation + transform.forward * moveSpeed);
        bodySpeed = moveInput * moveSpeed;
        transform.position += moveInput * transform.forward * moveSpeed;
    }

    void OnMovePerformed(CallbackContext context)
    {
        Vector2 inputVal = context.ReadValue<Vector2>();

        moveInput = inputVal.y;
        rotInput = inputVal.x;
    }

    void OnMoveCanceled(CallbackContext context)
    {
        moveInput = 0f;
        rotInput = 0f;
    }
}
