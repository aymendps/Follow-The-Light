using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallWallHoppingState : BallBaseState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Wall Hopping State");
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {

        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            /*
             * If the player does not try to bounce, the ball will slide with the wall until it reaches the ground
             * So we need to change the state to moving state when the ball touches the ground
             * The player is still allowed to attempt to bounce while sliding
             */
            if (collision.gameObject.layer == ballController.groundLayer)
            {
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballMovingState);
            }
        }

        public override void HandleInputAndConditions(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            if (!ballController.AttemptingToBounce) return;
            
            /*
             * If the player tries to bounce, perform the hopping action
             * The direction of the hop is slightly upwards and in the direction set by the input manager
             */
            ballController.RigidbodyComponent.AddForce(
                ballController.RotationRelativeToCamera * (Vector3.up + ballController.Direction) *
                ballController.jumpPower, ForceMode.VelocityChange);
                
            // Change the state to falling state after hopping
            ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballFallingState);
        }
    }
}