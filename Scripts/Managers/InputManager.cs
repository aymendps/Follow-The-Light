using Gameplay;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        [Tooltip("Reference to the ball controller")]
        [SerializeField] private BallController ballController;
        [Tooltip("The game object that the camera will follow and rotate around")]
        [SerializeField] private GameObject cameraTarget;
        
        // The last angle that the camera snapped to
        private float _lastSnappedAngle;
        // The camera angles that the camera will snap to when the player presses the "Fixed Look" action
        private static readonly float[] CameraAnglePresets = {0f, 90f, 180f, 270f, 360f};

        /*
         * All Input Actions are defined in the Input Actions asset.
         * The actions below will be called when the corresponding action is triggered.
         */
        
        private void OnMove(InputValue value)
        {
            var val = value.Get<Vector2>();
            ballController.Direction =  new Vector3(val.x, 0, val.y);
        }

        private void OnBounce(InputValue value)
        {
            ballController.AttemptingToBounce = value.Get<float>() > 0.1f;
        }

        private void OnBrake(InputValue value)
        {
            ballController.AttemptingToBrake = value.Get<float>() > 0.1f;
        }
        
        private void OnLook(InputValue value)
        {
            ballController.CameraRotationDirection = value.Get<float>();
        }
        
        private void OnFixedLook()
        {
            // If the camera is already snapped to a preset angle, rotate it 90 degrees
            if (Mathf.Approximately(_lastSnappedAngle, cameraTarget.transform.rotation.eulerAngles.y))
            {
                cameraTarget.transform.Rotate(0f, 90f, 0f, Space.World);
                _lastSnappedAngle = cameraTarget.transform.rotation.eulerAngles.y;
            }
            // Otherwise, snap the camera to the nearest preset angle
            else
            {
                _lastSnappedAngle = Algorithms.FindNearestAngle(CameraAnglePresets, cameraTarget.transform.rotation.eulerAngles.y);
                cameraTarget.transform.eulerAngles = new Vector3(45f, _lastSnappedAngle, 0f);
            } 
        }

    }
}