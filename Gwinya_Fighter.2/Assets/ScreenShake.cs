using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.2f;    // Duration of the screen shake
    public float shakeIntensity = 0.2f;   // Intensity of the screen shake
    private Vector3 originalPosition;

    private CinemachineVirtualCamera virtualCamera;    // Reference to the Cinemachine Virtual Camera component
    private CinemachineBasicMultiChannelPerlin noise;  // Reference to the Cinemachine Noise component

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        originalPosition = transform.localPosition;
    }

    public void Shake()
    {
        // Enable the noise effect with the desired intensity and duration
        Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;

        // Apply the shake effect to the camera's position
        transform.localPosition = shakePosition;

        // Invoke a method to stop the screen shake after the specified duration
    }

   
}
