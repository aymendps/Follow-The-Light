using System.Collections;
using System.Collections.Generic;
using CineMachine;
using Managers;
using UnityEngine;

namespace Main_Menu
{
    public class NormalLights : MonoBehaviour
    {
        [Tooltip("Normal lights of the lamp that will be burned")]
        [SerializeField] private List<Light> normalLights;
        
        [Tooltip("Short lamp object that has the normal lights")]
        [SerializeField] private GameObject shortLamp;

        [Tooltip("Static neon sound that will be stopped when the lamp is burned")]
        [SerializeField] private AudioSource staticNeonSound;
        
        [Tooltip("Sound that will be played when the lamp is burned")]
        [SerializeField] private AudioClip lightBurnedSound;

        /// <summary>
        /// Starts the burned animation
        /// </summary>
        public void BurnNormalLights()
        {
            StartCoroutine(BurnedAnimation());
        }

        // Defines the burned animation
        private IEnumerator BurnedAnimation()
        {
            // Disable all normal lights
            foreach (var l in normalLights)
            {
                l.enabled = false;
            }
            
            SoundEffectManager.Instance.PlaySoundEffect(lightBurnedSound);
            staticNeonSound.Stop();

            CineMachineShakeCamera.Instance.ShakeCamera();

            yield return new WaitForSeconds(1.2f);

            shortLamp.SetActive(false);
        }
    }
}