using Cinemachine;
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

    public void Look(Vector2 lookDirection)
    {
        Debug.Log(lookDirection);
        float deltaY = lookDirection.y;
        //camContainer.localRotation = Quaternion.Euler(deltaY * Time.deltaTime, 0f, 0f);
        //camContainer.rotation = Quaternion.LookRotation(lookDirection);
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
