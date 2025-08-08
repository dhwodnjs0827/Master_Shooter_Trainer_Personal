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
    public event Action OnADS;
    public event Action OnShoot;
    public event Action OnReload;

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
         actions.Move.performed += MovePerformed;
         actions.Move.canceled += MoveCanceled;
         
         actions.Look.performed += LookPerformed;
         actions.Look.canceled += LookCanceled;

         actions.Shoot.started += ShootStarted;
         
         actions.ADS.started += ADSStarted;
         
         actions.Reload.performed += ReloadStared;
    }

    private void RemoveInputs()
    {
        actions.Move.performed -= MovePerformed;
        actions.Move.canceled -= MoveCanceled;
        
        actions.Look.performed -= LookPerformed;
        actions.Look.canceled -= LookCanceled;
        
        actions.Shoot.started -= ShootStarted;
         
        actions.ADS.started -= ADSStarted;
         
        actions.Reload.started -= ReloadStared;
    }

    private void MovePerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void MoveCanceled(InputAction.CallbackContext context)
    {
        moveDirection = Vector2.zero;
    }

    private void LookPerformed(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    private void LookCanceled(InputAction.CallbackContext context)
    {
        lookDirection = Vector2.zero;
    }

    private void ShootStarted(InputAction.CallbackContext context)
    {
        OnShoot?.Invoke();
    }

    private void ADSStarted(InputAction.CallbackContext context)
    {
        OnADS?.Invoke();
    }

    private void ReloadStared(InputAction.CallbackContext context)
    {
        OnReload?.Invoke();
    }
}
