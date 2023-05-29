using UnityEngine;

namespace Main_Menu
{
    public class LoadingSwirlRotation : MonoBehaviour
    {
        [Tooltip("The rotation direction and speed of the loading swirl")]
        [SerializeField] private Vector3 rotationVector;

        // Rotate the loading swirl infinitely
        public void Update()
        {
            transform.Rotate(rotationVector);
        }
    }
}
