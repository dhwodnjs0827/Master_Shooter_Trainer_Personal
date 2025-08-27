using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Multiplier")]
    [SerializeField, Range(0f, 1f)] private float speedMultiplier = 0.15f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 inputDir, float speed)
    {
        Vector3 moveDir = new Vector3(inputDir.x, 0, inputDir.y).normalized;
        moveDir = transform.TransformDirection(moveDir);
        characterController.Move(moveDir * speed  * speedMultiplier * Time.deltaTime);
    }
}
