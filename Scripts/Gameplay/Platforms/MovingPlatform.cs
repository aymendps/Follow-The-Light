using UnityEngine;

namespace Gameplay.Platforms
{
    public class MovingPlatform : MonoBehaviour
    {
        [Tooltip("The distance the platform will move from its initial position")]
        [SerializeField] private Vector3 moveBy;
        
        [Tooltip("How fast the platform will move?")]
        [SerializeField] private float speed;
        
        [Tooltip("If true, the platform will wait for the player to enter the trigger before moving")]
        [SerializeField] private bool waitForPlayer;
        
        [Tooltip("The event that will be called when the ball enters the trigger")]
        [SerializeField] private BallEnterTriggerEvent ballEnterTriggerEvent;

        private Vector3 _destination;
        private Vector3 _initialPosition;
        
        private void Awake()
        {
            _initialPosition = transform.position;
            _destination = _initialPosition  + moveBy;
        }

        private void Start()
        {
            if (waitForPlayer) 
                ballEnterTriggerEvent.onBallEnterTrigger += StopWaitingForPlayer;
        }
        
        private void FixedUpdate()
        {
            MovePlatform();
        }

        // This method is called when the ball enters the trigger and we want to start moving the platform
        private void StopWaitingForPlayer()
        {
            waitForPlayer = false;
            ballEnterTriggerEvent.onBallEnterTrigger -= StopWaitingForPlayer;
        }

        private void MovePlatform()
        {
            // If we are waiting for the player to enter the trigger, we don't want to move the platform yet
            if (waitForPlayer) return;

            // Returns a value that is always increasing and decreasing between 0 and 1
            var interpolant = Mathf.PingPong(Time.time * speed, 1);
            
            // Use the interpolant to lerp between the initial position and the destination
            transform.position = Vector3.Lerp(_initialPosition, _destination, interpolant);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (other.transform.parent != null) return;
            
            // Set the player as a child of the platform so it moves with it
            other.transform.SetParent(transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            
            // Remove the player as a child of the platform so it doesn't move with it anymore
            other.transform.SetParent(null);
        }

    }
}
