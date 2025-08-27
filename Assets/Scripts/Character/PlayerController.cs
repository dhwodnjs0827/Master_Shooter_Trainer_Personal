using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 inputDir, float moveSpeed)
    {
        Vector3 moveDir = new Vector3(inputDir.x, 0, inputDir.y).normalized;
        moveDir = transform.TransformDirection(moveDir);
        // 0.3f: 이동속도 조절 상수
        characterController.Move(moveDir * moveSpeed  * 0.3f * Time.deltaTime);
    }
}
