using Cinemachine;
using DataDeclaration;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] private Transform camContainer;
    [SerializeField] private CinemachineVirtualCamera adsCamera;
    [SerializeField] private CinemachineVirtualCamera nonADSCamera;
    private bool isADS;

    private void Awake()
    {
        isADS = false;
        adsCamera.gameObject.SetActive(isADS);
        nonADSCamera.gameObject.SetActive(!isADS);
    }

    public void TriggerADS()
    {
        isADS = !isADS;
        adsCamera.gameObject.SetActive(isADS);
        nonADSCamera.gameObject.SetActive(!isADS);
    }

    public void Look(Vector2 inputDir)
    {
        // 상하 회전
        Vector3 curRot = camContainer.localEulerAngles;
        float rotX = curRot.x;
        // 0~360 범위를 -180~180도로 변환
        if (rotX > 180)
        {
            rotX -= 360f;
        }
        rotX -= inputDir.y * Constants.MOUSE_SENSITIVITY;
        rotX = Mathf.Clamp(rotX, Constants.MIN_MOUSE_ROT_Y, Constants.MAX_MOUSE_ROT_Y);
        camContainer.localEulerAngles = new Vector3(rotX, 0f, 0f);
        
        // 좌우 회전
        float deltaX = inputDir.x * Constants.MOUSE_SENSITIVITY;
        transform.Rotate(Vector3.up * deltaX);
    }

    public void ApplyRecoil(float recoil)
    {
        Debug.Log($"{recoil} 반동 적용");
    }

    public void ApplyStepShake(float step)
    {
        Debug.Log($"{step} STP 수치 적용");
    }
}
