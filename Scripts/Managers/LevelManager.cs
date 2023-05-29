using System.Collections;
using CineMachine;
using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }
        
        [Tooltip("Reference to the ball controller")]
        [SerializeField] private BallController ballController;
        
        [Tooltip("Reference to the camera target")]
        [SerializeField] private GameObject cameraTarget;
        
        [Tooltip("The material to use when a wall is glowing")]
        [SerializeField] private Material wallGlowingMaterial;
        
        /// <summary>
        /// The material to use when a wall is glowing
        /// </summary>
        public Material WallGlowingMaterial => wallGlowingMaterial;

        [Header("Respawn Settings")]
        
        [Tooltip("Where should the ball respawn?")]
        [SerializeField] private GameObject respawnPoint;
        [Tooltip("The particle effect to play when the ball respawns")]
        [SerializeField] private GameObject respawnParticleEffect;
        
        [Header("Death Settings")]
        
        [Tooltip("The particle effect to play when the ball dies")]
        [SerializeField] private GameObject deathParticleEffect;
        [Tooltip("The quote to play when the ball dies")]
        [SerializeField] private AudioClip deathQuote;
        [Tooltip("The sound effect to play when the ball dies")]
        [SerializeField] private AudioClip deathSound;
        [Tooltip("The panel to fade in and out when the ball dies")]
        [SerializeField] private Image deathPanel;
        [Tooltip("The text to fade in and out when the ball dies")]
        [SerializeField] private TextMeshProUGUI deathPanelText;
        [Tooltip("The speed at which the panel and text fade in and out")]
        [SerializeField] private float fadeSpeed;

        private void Awake()
        {
            //Singleton pattern with don't destroy on load
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else Destroy(gameObject);
        }

        /// <summary>
        /// Starts the level lost sequence
        /// </summary>
        public void LoseLevel()
        { 
            StartCoroutine(LevelLost());
        }

        // Defines the level lost sequence
        private IEnumerator LevelLost()
        {
          
            // Disable the ball renderers and play the death particle effect
            ballController.MeshRendererComponent.enabled = false;
            ballController.TrailRendererComponent.enabled = false;
            Instantiate(deathParticleEffect, ballController.transform.position, Quaternion.identity);
            
            // Reset the ball's rigidbody and disable gravity
            ballController.RigidbodyComponent.velocity = Vector3.zero;
            ballController.RigidbodyComponent.angularVelocity = Vector3.zero;
            ballController.RigidbodyComponent.useGravity = false;
            ballController.RigidbodyComponent.Sleep();

            SoundEffectManager.Instance.PlaySoundEffect(deathSound);
            CineMachineShakeCamera.Instance.ShakeCamera();
            
            // Fade in the death panel and text
            Fading.FadeInText(fadeSpeed, deathPanelText);
            Fading.FadeInImage(fadeSpeed, deathPanel);
            yield return new WaitForSeconds(0.75f);
            
            SoundEffectManager.Instance.PlaySoundEffect(deathQuote);
            yield return new WaitForSeconds(1.75f);
            
            // Start the level reset sequence
            StartCoroutine(ResetLevel()); 
        }

        // Defines the level reset sequence
        private IEnumerator ResetLevel()
        {
            // Reset the ball position and camera rotation
            cameraTarget.transform.rotation = Quaternion.Euler(45, 0, 0);
            ballController.transform.position = respawnPoint.transform.position;
            
            // Enable the ball renderers and gravity
            ballController.RigidbodyComponent.useGravity = true;
            ballController.MeshRendererComponent.enabled = true;
            ballController.TrailRendererComponent.enabled = true;

            // Fade out the death panel and text
            Fading.FadeOutText(fadeSpeed, deathPanelText);
            Fading.FadeOutImage(fadeSpeed, deathPanel);
            yield return new WaitForSeconds(0.4f);
            
            // Play the respawn particle effect
            Instantiate(respawnParticleEffect, ballController.transform.position, Quaternion.Euler(90,0,0));
        }
    }
}
