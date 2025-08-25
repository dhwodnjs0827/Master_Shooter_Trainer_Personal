using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputAction playerInputAction;
    private PlayerInputAction.PlayerActions actions;

    private Vector2 moveDirection;
    private Vector2 lookDirection;

    public Vector2 MoveDirection => moveDirection;
    public Vector2 LookDirection => lookDirection;

    public event Action OnMoveStarted;
    public event Action OnMove;
    public event Action OnMoveCanceled;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        actions = playerInputAction.Player;
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
        AddInputs();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
        RemoveInputs();
    }

    private void AddInputs()
    {
        actions.Move.started += MoveStarted;
        actions.Move.performed += MovePerformed;
        actions.Move.canceled += MoveCanceled;

        actions.Look.performed += LookPerformed;
        actions.Look.canceled += LookCanceled;
    }

    private void RemoveInputs()
    {
        actions.Move.started -= MoveStarted;
        actions.Move.performed -= MovePerformed;
        actions.Move.canceled -= MoveCanceled;

        actions.Look.performed -= LookPerformed;
        actions.Look.canceled -= LookCanceled;
    }

    private void MoveStarted(InputAction.CallbackContext context)
    {
        OnMoveStarted?.Invoke();
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
        OnMove?.Invoke();
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
        OnMoveCanceled?.Invoke();
    }

    private void LookPerformed(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    private void LookCanceled(InputAction.CallbackContext context)
    {
        lookDirection = Vector2.zero;
    }
}
