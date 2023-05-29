using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallBouncingState : BallBaseState
    {
        private Vector3 _lastBouncePosition = Vector3.positiveInfinity;
        private float _lastBounceTime;
        private int _bouncesInARow;
        private float _bouncePowerMultiplier = 1f;

        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Bouncing State");
            
            // If the ball is still in the same position and the time between bounces is in the allowed time window
            if (_lastBounceTime + ballController.bounceTimeWindow >= Time.time &&
                Vector3.Distance(ballController.transform.position, _lastBouncePosition)
                <= ballController.bounceRadius / 2f)
            {
                // If the ball has not reached the max bounces in a row
                if (_bouncesInARow < ballController.maxBouncesInARow)
                {
                    // Increase the bounce power multiplier and the bounces in a row counter
                    _bouncePowerMultiplier += ballController.bounciness;
                    _bouncesInARow++;
                }
            }
            // If the ball is not in the same position or the time between bounces is not in the allowed time window
            else
            {
                // Reset the bounces in a row counter and the bounce power multiplier
                _bouncesInARow = 0;
                _bouncePowerMultiplier = 1f;
            }

            // Keep track of the last bounce position and time
            _lastBouncePosition = ballController.transform.position;
            _lastBounceTime = Time.time;

            // Perform the bounce action
            ballController.RigidbodyComponent.AddForce(Vector3.up * (ballController.jumpPower * _bouncePowerMultiplier),
                ForceMode.VelocityChange);
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            
        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            
        }

        public override void HandleInputAndConditions(BallController ballController,
            BallFiniteStateMachine ballFiniteStateMachine)
        {
            if (!ballController.IsGrounded()) 
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballFallingState);
        }
    }
}