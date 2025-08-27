using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    private PlayerInputHandler inputHandler;
    private PlayerController controller;
    private PlayerCameraHandler cameraHandler;
    private PlayerStatHandler stat;
    private PlayerEquipment equipment;

    private bool isMoving = false;
    private bool isMoveTransition = false;
    private bool isADS = false;

    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerStatHandler Stat => stat;
    public PlayerEquipment Equipment => equipment;

    public bool IsMoving => isMoving;
    public bool IsMoveTransition => isMoveTransition;
    public bool IsADS => isADS;
    public event Action<bool> OnADS;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<PlayerController>();
        cameraHandler = GetComponent<PlayerCameraHandler>();
        equipment = GetComponent<PlayerEquipment>();
        stat = new PlayerStatHandler(playerSO);

        equipment.EquipWeapon();
    }

    private void OnEnable()
    {
        inputHandler.OnADSStarted += TriggerADS;
        inputHandler.OnShootStarted += Shoot;
    }

    private void Start()
    {
        cameraHandler.SetWeaponCameraPosition(equipment.Weapon.CameraPoint);
    }

    private void Update()
    {
        CheckMoving();
        controller.Move(inputHandler.MoveDirection, stat.Speed);
    }

    private void LateUpdate()
    {
        cameraHandler.Look(inputHandler.LookDirection);
    }

    private void OnDisable()
    {
        inputHandler.OnADSStarted -= TriggerADS;
        inputHandler.OnShootStarted -= Shoot;
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
        OnADS?.Invoke(isADS);
    }

    private void Shoot()
    {
        equipment.Weapon.Shoot();
        StartCoroutine(cameraHandler.ApplyRecoil(stat.Recoil));
    }
}
