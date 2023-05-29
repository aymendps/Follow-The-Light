using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Main_Menu
{
    public class MainMenuDirector : MonoBehaviour
    {
        [Tooltip("Reference to the MainMenuUI script")]
        [SerializeField] private MainMenuUI mainMenuUI;
        
        [Tooltip("Reference to the FlickeringLight script")]
        [SerializeField] private FlickeringLight flickeringLight;
        
        [Tooltip("Reference to the NormalLights script")]
        [SerializeField] private NormalLights normalLights;

        [Tooltip("Reference to the Animator component of the CineMachine camera")]
        [SerializeField] private Animator cineMachineAnimator;
        private static readonly int ViewIndexProperty = Animator.StringToHash("View Index");
        
        [Tooltip("The rain particle system")] 
        [SerializeField] private ParticleSystem rainParticleSystem;
        [Tooltip("The rain audio source")] 
        [SerializeField] private AudioSource rainAudioSource;

        [Tooltip("Reference to the ball model in the main menu")]
        [SerializeField] private MainMenuBallModel mainMenuBallModel;

        [Tooltip("The game object to show when loading the game")]
        [SerializeField] private GameObject loadingSwirl;

        private void Start()
        {
            // Move CineMachine camera to the next view and show the menu UI
            cineMachineAnimator.SetInteger(ViewIndexProperty, 1);
            StartCoroutine(mainMenuUI.ShowMenuUI());
        }

        private void Update()
        {
            mainMenuUI.UpdateTitleColor(mainMenuBallModel.LightColor);
        }

        // Play the transition sequence from the main menu to the game
        private IEnumerator PlayTransitionSequence()
        {
            mainMenuUI.HideMenuUI();

            yield return new WaitForSeconds(0.5f);

            flickeringLight.BurnFlickeringLight();

            mainMenuBallModel.StartExpandingLight();

            yield return new WaitForSeconds(2.2f);

            normalLights.BurnNormalLights();

            // Stop the rain particle system and the rain audio source
            var emission = rainParticleSystem.emission;
            emission.rateOverTime = 0f;
            rainAudioSource.Stop();

            yield return new WaitForSeconds(1.2f);

            mainMenuBallModel.StartMoving();
            
            yield return new WaitForSeconds(1.5f);

            // Move CineMachine camera to the next view
            cineMachineAnimator.SetInteger(ViewIndexProperty, 2);

            yield return new WaitForSeconds(0.5f);
            
            // Show the loading swirl
            loadingSwirl.SetActive(true);

            yield return new WaitForSeconds(2f);

            // Start loading the next scene asynchronously
            yield return SceneLoader.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        /// Called when the Play button is pressed
        /// </summary>
        public void PlayButton()
        {
            StartCoroutine(PlayTransitionSequence());
        }
        
        /// <summary>
        /// Called when the Exit button is pressed
        /// </summary>
        public void ExitButton()
        {
            Application.Quit();
        }
    }
}