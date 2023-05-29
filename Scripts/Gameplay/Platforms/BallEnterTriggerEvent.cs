using UnityEngine;

namespace Gameplay.Platforms
{
    /// <summary>
    /// This class is used to inform platforms that the ball has entered this trigger collider.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class BallEnterTriggerEvent : MonoBehaviour
    { 
        // delegate to be invoked when the ball enters the trigger collider
        public delegate void OnBallEnterTrigger();
        public OnBallEnterTrigger onBallEnterTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (onBallEnterTrigger == null) return;
            
            onBallEnterTrigger.Invoke();
            Destroy(gameObject);
        }
    }
}
