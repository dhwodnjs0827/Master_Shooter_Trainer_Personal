using System.Collections;
using Cinemachine;
using DataDeclaration;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform cameraContainer;
    [SerializeField] private Transform armModel;

    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera weaponCamera;

    private CinemachineBasicMultiChannelPerlin currentSteopShakeNoise;
    private CinemachineBasicMultiChannelPerlin playerStepShakeNoise;
    private CinemachineBasicMultiChannelPerlin weaponStepShakeNoise;

    private float stepNoiseAmplitudeGain = 0.14f;
    private Coroutine stepFadeCoroutine;

    private Vector3 initArmModelRotaton;

    [Header("Multiplier")]
    [SerializeField, Range(0f, 0.01f)] private float stepNoiseMultiplier = 0.006f;
    [SerializeField, Range(0f, 1f)] private float recoilMultiplier = 1f;
    [SerializeField, Range(0f, 1f)] private float shakeMultiplier = 0.5f;


    private void Awake()
    {
        player = GetComponent<Player>();
        playerStepShakeNoise = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        weaponStepShakeNoise = weaponCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        currentSteopShakeNoise = playerStepShakeNoise;

        initArmModelRotaton = armModel.eulerAngles;
    }

    private void OnEnable()
    {
        player.OnADS += ChangeCurrentCamera;
    }

    private void Update()
    {
        StepNoise();
        HandShake(player.Stat.Handling);
    }

    private void OnDisable()
    {
        player.OnADS -= ChangeCurrentCamera;
    }

    public void Look(Vector2 inputDir)
    {
        // 상하 회전
        Vector3 curRotation = cameraContainer.localEulerAngles;
        float rotationX = curRotation.x;
        // 0~360 범위를 -180~180도로 변환
        if (rotationX > 180)
        {
            rotationX -= 360f;
        }
        rotationX -= inputDir.y * Constants.MOUSE_SENSITIVITY;
        rotationX = Mathf.Clamp(rotationX, Constants.MIN_MOUSE_ROT_Y, Constants.MAX_MOUSE_ROT_Y);
        cameraContainer.localEulerAngles = new Vector3(rotationX, 0f, 0f);

        // 좌우 회전
        float deltaX = inputDir.x * Constants.MOUSE_SENSITIVITY;
        transform.Rotate(Vector3.up * deltaX);
    }

    public void SetWeaponCameraPosition(Transform virtualCamera)
    {
        weaponCamera.transform.position = virtualCamera.position;
    }

    private IEnumerator FadeStepShakeIn(float stepValue)
    {
        // step 수치에 따른 흔들림 계산
        float stepAmount = (Constants.MAX_STAT_VALUE - stepValue) * stepNoiseMultiplier;

        float currentAmplitude = currentSteopShakeNoise.m_AmplitudeGain;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            weaponStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, stepAmount, elapsed / duration);
            playerStepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, stepAmount, elapsed / duration);
            yield return null;
        }
        playerStepShakeNoise.m_AmplitudeGain = stepAmount;
        weaponStepShakeNoise.m_AmplitudeGain = stepAmount;
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
                stepFadeCoroutine = StartCoroutine(FadeStepShakeIn(player.Stat.Step));
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

    public IEnumerator ApplyRecoil(float recoilValue)
    {
        float recoilAmount = (Constants.MAX_STAT_VALUE - recoilValue) * recoilMultiplier;
        Vector3 currentRotation = cameraContainer.localEulerAngles;
        Vector3 targetRotation = new Vector3(currentRotation.x - recoilAmount, 0f, 0f);
        float duration = 0.05f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cameraContainer.localEulerAngles = Vector3.Lerp(currentRotation, targetRotation, elapsed / duration);
            yield return null;
        }
        cameraContainer.localEulerAngles = targetRotation;
    }

    public void HandShake(float handlingValue)
    {
        float shakeAmount = (Constants.MAX_STAT_VALUE - handlingValue) * shakeMultiplier;
        float rotX = (Mathf.PerlinNoise(Time.time * 0.7f, 0f) - 0.5f) * shakeAmount;
        float rotY = (Mathf.PerlinNoise(0f, Time.time * 0.7f) - 0.5f) * shakeAmount;
        armModel.localEulerAngles = initArmModelRotaton + new Vector3(rotX, rotY, 0f);
    }
}
