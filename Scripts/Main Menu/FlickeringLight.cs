using System.Collections;
using CineMachine;
using Gameplay;
using Managers;
using UnityEngine;

namespace Main_Menu
{
    public class FlickeringLight : MonoBehaviour
    {
        [Header("Flickering Light Settings")]
        
        [Tooltip("The intensity of the light when it flickers")]
        [SerializeField] private float lightFlickerIntensity;
        
        [Tooltip("The number of times the light will flicker")]
        [SerializeField] private float flickerTimes;
        
        [Tooltip("The duration of a single flicker")]
        [SerializeField] private float flickerDuration;
        
        [Tooltip("The variance to the duration of a single flicker")]
        [SerializeField] private float varianceToDuration;
        
        [Tooltip("The time between flickers")]
        [SerializeField] private float timeBetweenFlickers;
        
        [Header("Flickering Light Sounds")]
        
        [Tooltip("The sound played when the light flickers on")]
        [SerializeField] private AudioClip flickerOnSound;
        
        [Tooltip("The sound played when the light flickers off")]
        [SerializeField] private AudioClip flickerOffSound;
        
        [Tooltip("The sound played when the light is burned")]
        [SerializeField] private AudioClip lightBurnedSound;
        
        private Light _lightComponent;
        private float _defaultIntensity;
        private float _minFlickerDuration;
        private float _maxFlickerDuration;

        private void Start()
        {
            // Get the light component and save the default intensity
            _lightComponent = GetComponent<Light>();
            _defaultIntensity = _lightComponent.intensity;
            
            // Calculate the min and max duration of a flicker
            _minFlickerDuration = flickerDuration - varianceToDuration;
            _maxFlickerDuration = flickerDuration + varianceToDuration;
            
            // Start the flickering animation
            StartCoroutine(FlickeringForeverAnimation());
        }
        
        /// <summary>
        /// Stops the flickering animation and plays the burned animation
        /// </summary>
        public void BurnFlickeringLight()
        {
            // Stop the flickering forever animation
            this.StopAllCoroutines();
            // Start the burned animation
            StartCoroutine(BurnedAnimation());
        }

        // Defines the flickering animation
        private IEnumerator FlickeringAnimation()
        {
            for(int i=0; i<flickerTimes; i++)
            {
                // Flicker the light
                _lightComponent.intensity = lightFlickerIntensity;
                SoundEffectManager.Instance.PlaySoundEffect(flickerOffSound);

                // Wait for a random duration
                yield return new WaitForSeconds(Random.Range(_minFlickerDuration,_maxFlickerDuration));

                // Restore the light to default
                _lightComponent.intensity = _defaultIntensity;
                SoundEffectManager.Instance.PlaySoundEffect(flickerOnSound);

                // Wait for a random duration
                yield return new WaitForSeconds(Random.Range(_minFlickerDuration, _maxFlickerDuration));
            }
        }

        // Defines the flickering forever animation
        private IEnumerator FlickeringForeverAnimation()
        {
            while (true)
            {
                // Wait for the flickering animation to finish
                yield return StartCoroutine(FlickeringAnimation());
                
                // Wait for the time between flickers
                yield return new WaitForSeconds(timeBetweenFlickers);
            }
        }

        // Defines the burned animation
        private IEnumerator BurnedAnimation()
        {
            // Wait for the flickering animation to finish
            yield return StartCoroutine(FlickeringAnimation());
            
            _lightComponent.enabled = false;
            SoundEffectManager.Instance.PlaySoundEffect(lightBurnedSound);
            
            CineMachineShakeCamera.Instance.ShakeCamera();
        }
    }
}
