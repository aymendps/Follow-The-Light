using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallMovingState : BallOnGroundState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Moving State");
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        { 
            // Move the ball in the direction set by the input manager
            ballController.RigidbodyComponent.AddForce(ballController.RotationRelativeToCamera * ballController.Direction *
                                                     ballController.movementSpeed);
        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            
        }

        public override void HandleInputAndConditions(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            if (ballController.AttemptingToBrake)
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballBrakingState);
            else base.HandleInputAndConditions(ballController, ballFiniteStateMachine);
        }
    }
}