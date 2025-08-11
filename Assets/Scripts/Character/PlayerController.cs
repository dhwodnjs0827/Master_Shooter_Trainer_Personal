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
        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        Debug.Log("총기 발사");
    }

    public void Reload()
    {
        Debug.Log("재장전");
    }
}
