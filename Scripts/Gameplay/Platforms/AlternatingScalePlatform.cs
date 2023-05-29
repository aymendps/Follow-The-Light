using UnityEngine;

namespace Gameplay.Platforms
{
    public class AlternatingScalePlatform : MonoBehaviour
    {
        [Tooltip("How fast the platform will resize?")]
        [SerializeField] private float speed;
        
        [Tooltip("The scale the platform will reach when it's resizing")]
        [SerializeField] private Vector3 desiredScale;

        private Vector3 _initialScale;

        private void Awake()
        {
            _initialScale = transform.localScale;
        }

        private void Update()
        {
            ResizeScale();
        }
        
        private void ResizeScale()
        {
            // Returns a value that is always increasing and decreasing between 0 and 1
            var interpolant = Mathf.PingPong(Time.time * speed, 1);
            
            // Use the interpolant to lerp between the initial scale and the desired scale
            transform.localScale = Vector3.Lerp(_initialScale, desiredScale, interpolant);
        }
    }
}
