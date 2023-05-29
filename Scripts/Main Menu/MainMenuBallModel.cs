using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Main_Menu
{
    public class MainMenuBallModel : MonoBehaviour
    {
        [Tooltip("The initial speed of the ball model")]
        [SerializeField] private float ballModelInitialSpeed;
        
        [Tooltip("The final speed of the ball model")]
        [SerializeField] private float ballModelFinalSpeed;
        
        private Light _lightComponent;
        public Color LightColor => _lightComponent.color;
        private Rigidbody _rigidbody;
        private TrailRenderer _trailRenderer;
        private float _maximumSpeed;
        
        private void Awake()
        {
            _lightComponent = GetComponent<Light>();
            _rigidbody = GetComponent<Rigidbody>();
            _trailRenderer = GetComponent<TrailRenderer>();
        }
        private void Update()
        {
            _trailRenderer.material.color = _lightComponent.color;
        }
        private void FixedUpdate()
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maximumSpeed);
        }
        
        /// <summary>
        /// Starts the move animation of the ball model
        /// </summary>
        public void StartMoving()
        {
            StartCoroutine(MoveAnimation());
        }
        
        /// <summary>
        /// Starts the expanding light animation of the ball model
        /// </summary>
        public void StartExpandingLight()
        {
            StartCoroutine(ExpandLight());
        }

        // Tween the range and intensity of the glow effect of the ball model
        private IEnumerator ExpandLight()
        {
            // Tween the range of the glow effect of the ball model
            DOTween.To(() => _lightComponent.range, x => _lightComponent.range = x, 0.5f, 4);

            yield return new WaitForSeconds(2.2f);
            
            // Tween the intensity of the glow effect of the ball model
            DOTween.To(() => _lightComponent.intensity, x => _lightComponent.intensity = x, 2.5f, 1);
        }

        // Tween the maximum speed of the ball model and enable gravity
        private IEnumerator MoveAnimation()
        {   
            _rigidbody.useGravity = true;
            _maximumSpeed = ballModelInitialSpeed;
            yield return new WaitForSeconds(2f);
            _maximumSpeed = ballModelFinalSpeed;
        }
    }
}
