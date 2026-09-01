using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] ParticleSystem speedUpEffect;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float minFOV = 20f;
    [SerializeField] float maxFOV = 120f;
    [SerializeField] float zoomSpeedModifier = 5f;
    CinemachineCamera cinemachineCamera;

    private void Awake() {
        cinemachineCamera = GetComponent<CinemachineCamera>();   
    }
    public void ChangeCameraFOV(float speedAmount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        if (speedAmount > 0)
        {
            speedUpEffect.Play();
        }
        else
        {
            speedUpEffect.Stop();
        }
    }


    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speedAmount * zoomSpeedModifier, minFOV, maxFOV);


        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
