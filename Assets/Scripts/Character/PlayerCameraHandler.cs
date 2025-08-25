using System.Collections;
using Cinemachine;
using DataDeclaration;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform armModel;

    [SerializeField] private CinemachineVirtualCamera playerCamera;
    private CinemachineVirtualCamera weaponCamera;

    private CinemachineBasicMultiChannelPerlin currentSteopShakeNoise;
    private CinemachineBasicMultiChannelPerlin playerStepShakeNoise;
    private CinemachineBasicMultiChannelPerlin weaponStepShakeNoise;

    private float stepNoiseAmplitudeGain = 0.14f;
    private Coroutine stepFadeCoroutine;


    private void Awake()
    {
        player = GetComponent<Player>();
        playerStepShakeNoise = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        currentSteopShakeNoise = playerStepShakeNoise;
    }

    private void OnEnable()
    {
        player.OnADS += ChangeCurrentCamera;
    }

    private void Update()
    {
        StepNoise();
    }

    private void OnDisable()
    {
        player.OnADS -= ChangeCurrentCamera;
    }

    public void Look(Vector2 inputDir)
    {
        // 상하 회전
        Vector3 curRot = armModel.localEulerAngles;
        float rotX = curRot.x;
        // 0~360 범위를 -180~180도로 변환
        if (rotX > 180)
        {
            rotX -= 360f;
        }
        rotX -= inputDir.y * Constants.MOUSE_SENSITIVITY;
        rotX = Mathf.Clamp(rotX, Constants.MIN_MOUSE_ROT_Y, Constants.MAX_MOUSE_ROT_Y);
        armModel.localEulerAngles = new Vector3(rotX, 0f, 0f);

        // 좌우 회전
        float deltaX = inputDir.x * Constants.MOUSE_SENSITIVITY;
        transform.Rotate(Vector3.up * deltaX);
    }

    public void RegisterWeaponCamera(CinemachineVirtualCamera virtualCamera)
    {
        weaponCamera = virtualCamera;
        weaponStepShakeNoise = weaponCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private IEnumerator FadeStepShakeIn()
    {
        // step 수치에 따른 흔들림 계산
        //stepNoiseAmplitudeGain = 0.14f * (1f - stepValue / 100f);

        float currentAmplitude = currentSteopShakeNoise.m_AmplitudeGain;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            weaponStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, stepNoiseAmplitudeGain, elapsed / duration);
            playerStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, stepNoiseAmplitudeGain, elapsed / duration);
            yield return null;
        }
        playerStepShakeNoise.m_AmplitudeGain = stepNoiseAmplitudeGain;
        weaponStepShakeNoise.m_AmplitudeGain = stepNoiseAmplitudeGain;
        stepFadeCoroutine = null;
    }

    private IEnumerator FadeStepShakeOut()
    {
        float currentAmplitude = currentSteopShakeNoise.m_AmplitudeGain;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            weaponStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, 0f, elapsed / duration);
            playerStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, 0f, elapsed / duration);
            yield return null;
        }
        playerStepShakeNoise.m_AmplitudeGain = 0f;
        weaponStepShakeNoise.m_AmplitudeGain = 0f;
        stepFadeCoroutine = null;
    }

    private void StepNoise()
    {
        if (player.IsMoveTransition)
        {
            if (stepFadeCoroutine != null)
            {
                StopCoroutine(stepFadeCoroutine);
            }
            if (player.IsMoving)
            {
                stepFadeCoroutine = StartCoroutine(FadeStepShakeIn());
            }
            else
            {
                stepFadeCoroutine = StartCoroutine(FadeStepShakeOut());
            }
        }
    }

    private void ChangeCurrentCamera(bool isADS)
    {
        currentSteopShakeNoise = isADS ? weaponStepShakeNoise : playerStepShakeNoise;
        playerCamera.gameObject.SetActive(!isADS);
        weaponCamera.gameObject.SetActive(isADS);
    }
}
