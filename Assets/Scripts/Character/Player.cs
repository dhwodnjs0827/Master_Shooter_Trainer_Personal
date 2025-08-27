using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    private PlayerInputHandler inputHandler;
    private PlayerMovement movement;
    private PlayerCameraHandler cameraHandler;
    private PlayerStatHandler stat;
    private PlayerEquipment equipment;

    private bool isMoving = false;
    private bool isMoveTransition = false;
    private bool isADS = false;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        movement = GetComponent<PlayerMovement>();
        cameraHandler = GetComponent<PlayerCameraHandler>();
        stat = GetComponent<PlayerStatHandler>();
        equipment = GetComponent<PlayerEquipment>();

        stat.Init(playerSO);
    }

    private void OnEnable()
    {
        inputHandler.OnADSStarted += TriggerADS;
    }

    private void Start()
    {
        var weapon = equipment.EquipWeapon();
        inputHandler.OnShootStarted += () => weapon.Shoot(stat.Recoil);
        cameraHandler.Init(weapon);
    }

    private void Update()
    {
        CheckMoving();
        movement.Move(inputHandler.MoveDirection, stat.Speed);
        cameraHandler.StepNoise(stat.Step, isMoveTransition, isMoving);
        cameraHandler.HandShake(stat.Handling);
    }

    private void LateUpdate()
    {
        cameraHandler.Look(inputHandler.LookDirection);
    }

    private void OnDisable()
    {
        inputHandler.OnADSStarted -= TriggerADS;
    }

    private void CheckMoving()
    {
        var currentMoveInput = inputHandler.MoveDirection.magnitude;

        if (isMoving)
        {
            if (currentMoveInput > 0f)
            {
                isMoving = true;
                isMoveTransition = false;
            }
            else
            {
                isMoving = false;
                isMoveTransition = true;
            }
        }
        else
        {
            if (currentMoveInput > 0f)
            {
                isMoving = true;
                isMoveTransition = true;
            }
            else
            {
                isMoving = false;
                isMoveTransition = false;
            }
        }
    }

    private void TriggerADS()
    {
        isADS = !isADS;
        cameraHandler.ChangeCurrentCamera(isADS);
    }
}
