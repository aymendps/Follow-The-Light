using Cinemachine;
using UnityEngine;

namespace CineMachine
{
    public class CineMachineShakeCamera : MonoBehaviour
    {
        public static CineMachineShakeCamera Instance { get; private set; }
        
        [Tooltip("How much will the camera shake?")]
        [SerializeField] private float shakeIntensity;
        
        [Tooltip("How long will the camera shake?")]
        [SerializeField] private float shakeTimer;

        // CineMachine noise component
        private CinemachineBasicMultiChannelPerlin _noise;
        
        // Timer for the shake
        private float _timer;

        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
            {
                _noise = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _timer = 0;
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            // If the timer is 0 or less, return
            if (!(_timer > 0)) return;
            
            // Decrease the timer
            _timer -= Time.deltaTime;
            
            // Set the noise amplitude to a value between shakeIntensity and 0 based on the timer
            _noise.m_AmplitudeGain = Mathf.Lerp(shakeIntensity, 0f, 1 - (_timer / shakeTimer));
        }
        
        public void ShakeCamera()
        {
            _noise.m_AmplitudeGain = shakeIntensity;
            _timer = shakeTimer;
        }
        
    }
}
