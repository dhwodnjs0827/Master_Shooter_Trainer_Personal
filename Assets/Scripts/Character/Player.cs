using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerSO playerSO;
    private PlayerInputHandler inputHandler;
    private PlayerController controller;
    private PlayerCameraHandler cameraHandler;
    private PlayerStatHandler stat;

    public PlayerInputHandler InputHandler => inputHandler;
    public PlayerStatHandler Stat => stat;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        controller = GetComponent<PlayerController>();
        cameraHandler = GetComponent<PlayerCameraHandler>();
        stat = new PlayerStatHandler(playerSO);
    }

    private void Update()
    {
        controller.Move(inputHandler.MoveDirection, stat.Speed);
    }

    private void LateUpdate()
    {
        cameraHandler.Look(inputHandler.LookDirection);
    }
}
