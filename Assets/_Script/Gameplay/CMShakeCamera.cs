using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMShakeCamera : MonoBehaviour
{
    public static CMShakeCamera instance;
    public float shakeIntensity;
    public float shakeTimer;

    CinemachineBasicMultiChannelPerlin noise;
    float timer;

    private void Awake()
    {
        instance = this;
        noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        timer = 0;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            noise.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (timer / shakeTimer));
        }
    }

    public void ShakeCamera()
    {
        noise.m_AmplitudeGain = shakeIntensity;
        timer = shakeTimer;
    }

    

}
