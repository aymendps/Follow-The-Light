using Gameplay.Ball_Finite_State_Machine;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class BallController : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private GameObject cameraTarget;
        public float cameraRotationSpeed;

        [Header("Movement Settings")]
        public float movementSpeed;
        public float brakingForce;

        [Header("Jump Settings")]
        public float jumpPower;
        public float distanceToGround;
        [Range(0f, 1f)] public float bounciness;
        [Range(1f,5f)] public float bounceTimeWindow;
        public float bounceRadius;
        public int maxBouncesInARow;
        
        [Header("Fall Settings")]
        [Range(2f, 20f)] public float airResistance;
        
        [Header("Death Settings")]
        public float dieWhenBelowY;

        [Header("Layers & Tags")]
        public string targetGlowTag;
        public int hoppingWallLayer;
        public int groundLayer;
        
        private BallFiniteStateMachine _finiteStateMachine;
        
        /// <summary>
        /// The direction the ball is moving in
        /// </summary>
        public Vector3 Direction { get; set; }
        /// <summary>
        /// The rotation to apply to the ball's direction to make it relative to the camera
        /// </summary>
        public Quaternion RotationRelativeToCamera { get; private set; }
        /// <summary>
        /// The direction the camera is rotating in (left or right)
        /// </summary>
        public float CameraRotationDirection { get; set; }
        
        /// <summary>
        /// The rigidbody component of the ball
        /// </summary>
        public Rigidbody RigidbodyComponent { get; private set; }
        /// <summary>
        /// The mesh renderer component of the ball
        /// </summary>
        public MeshRenderer MeshRendererComponent { get; private set; }
        /// <summary>
        /// The trail renderer component of the ball
        /// </summary>
        public TrailRenderer TrailRendererComponent { get; private set; }
        
        /// <summary>
        /// Returns true if the "Bounce" button is being pressed, otherwise false
        /// </summary>
        public bool AttemptingToBounce { get; set; }
        /// <summary>
        /// Returns true if the "Brake" button is being pressed, otherwise false
        /// </summary>
        public bool AttemptingToBrake { get; set; }
        
        private Light _ballLight;
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            
            var position = transform.position;
            var colliderBounds = GetComponent<SphereCollider>().bounds;

            // Draw the rays from the center of the collider to the ground
            Gizmos.DrawRay(position, Vector3.down * distanceToGround);

            // Draw the rays from the corners of the collider to the ground
            Gizmos.DrawRay(position + new Vector3(colliderBounds.extents.x, 0, 0), Vector3.down * distanceToGround);
            Gizmos.DrawRay(position - new Vector3(colliderBounds.extents.x, 0, 0), Vector3.down * distanceToGround);
            Gizmos.DrawRay(position + new Vector3(0, 0, colliderBounds.extents.z), Vector3.down * distanceToGround);
            Gizmos.DrawRay(position - new Vector3(0, 0, colliderBounds.extents.z), Vector3.down * distanceToGround);
        }
        
        private void Awake()
        {
            _finiteStateMachine = new BallFiniteStateMachine(this);
            
            _ballLight = GetComponent<Light>();
            TrailRendererComponent = GetComponent<TrailRenderer>();
            
            RigidbodyComponent = GetComponent<Rigidbody>();
            MeshRendererComponent = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            _finiteStateMachine.Start();
        }
        
        private void Update()
        {
            UpdateCameraTarget();
            UpdateTrail();

            _finiteStateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            _finiteStateMachine.FixedUpdate();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag(targetGlowTag))
            {
                collision.gameObject.GetComponent<WallGlowMaterial>().StartGlowing(_ballLight.color);
                WallGlowSoundManager.Instance.PlayWallSoundEffect();
            }
            
            _finiteStateMachine.OnCollisionEnter(collision);
        }
        
        private void UpdateCameraTarget()
        {
            // Update the position of the camera target to the position of the ball
            cameraTarget.transform.position = transform.position;
            
            // Rotate the camera target in the direction set by the input manager
            cameraTarget.transform.Rotate(0f, CameraRotationDirection * cameraRotationSpeed * Time.deltaTime,
                0f, Space.World);
            
            // The camera and the target are always facing each other, so the camera's rotation is the same as the target's
            RotationRelativeToCamera = Quaternion.Euler(0f, cameraTarget.transform.rotation.eulerAngles.y, 0f);
        }
        
        private void UpdateTrail()
        {
            TrailRendererComponent.material.color = _ballLight.color;
        }

        /// <summary>
        /// Returns true if the ball is on the ground, otherwise false
        /// </summary>
        /// <returns></returns>
        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, distanceToGround);
        }

    }
}