using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallBrakingState : BallOnGroundState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Braking State");
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Apply braking force opposite to the direction of the ball's velocity
            ballController.RigidbodyComponent.AddForce(-ballController.RigidbodyComponent.velocity *
                                                       ballController.brakingForce);
                
            // If the ball's velocity is less than 0.1, stop the ball completely
            if (ballController.RigidbodyComponent.velocity.sqrMagnitude < 0.1f)
                ballController.RigidbodyComponent.velocity = Vector3.zero;
        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            
        }

        public override void HandleInputAndConditions(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            if (!ballController.AttemptingToBrake)
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballMovingState);
            else base.HandleInputAndConditions(ballController, ballFiniteStateMachine);
        }
    }
}