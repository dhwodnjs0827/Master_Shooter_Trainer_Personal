using System.Collections;
using Cinemachine;
using DataDeclaration;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
    private Player player;

    [Header("Virtual Cam")]
    [SerializeField] private Transform armModel;
    [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;

    [Header("Step Shake")]
    [SerializeField] private CinemachineBasicMultiChannelPerlin stepShakeNoise;
    private float stepNoiseFrequencyGain = 0.028f;
    private float stepNoiseAmplitudeGain = 0.14f;
    private Coroutine stepFadeCoroutine;
    private bool isMoving;
    private bool isMoveTransition;

    private void Awake()
    {
        player = GetComponent<Player>();
        stepShakeNoise = playerVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        stepShakeNoise.m_FrequencyGain = stepNoiseFrequencyGain;
        isMoving = false;
        isMoveTransition = false;
    }

    private void Update()
    {
        CheckMoving();
        if (isMoveTransition)
        {
            if (stepFadeCoroutine != null)
            {
                StopCoroutine(stepFadeCoroutine);
            }
            if (isMoving)
                {
                    stepFadeCoroutine = StartCoroutine(FadeStepShakeIn());
                }
                else
                {
                    stepFadeCoroutine = StartCoroutine(FadeStepShakeOut());
                }
        }
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

    private IEnumerator FadeStepShakeIn()
    {
        // step 수치에 따른 흔들림 계산
        //stepNoiseAmplitudeGain = 0.14f * (1f - stepValue / 100f);

        float currentAmplitude = stepShakeNoise.m_AmplitudeGain;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            stepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, stepNoiseAmplitudeGain, elapsed/duration);
            yield return null;
        }
        stepShakeNoise.m_AmplitudeGain = stepNoiseAmplitudeGain;
        stepFadeCoroutine = null;
    }

    private IEnumerator FadeStepShakeOut()
    {
        float currentAmplitude = stepShakeNoise.m_AmplitudeGain;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            stepShakeNoise.m_AmplitudeGain = Mathf.Lerp(currentAmplitude, 0f, elapsed / duration);
            yield return null;
        }
        stepShakeNoise.m_AmplitudeGain = 0f;
        stepFadeCoroutine = null;
    }

    private void CheckMoving()
    {
        var currentMoveInput = player.InputHandler.MoveDirection.magnitude;

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
}
