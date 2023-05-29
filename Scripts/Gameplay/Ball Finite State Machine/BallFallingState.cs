using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallFallingState : BallBaseState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Falling State");
        }
        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            /*
             * Move the ball in the direction set by the input manager
             * Since the ball is in the air, the ball will move slower than it would on the ground
             */
            ballController.RigidbodyComponent.AddForce(ballController.RotationRelativeToCamera * ballController.Direction *
                ballController.movementSpeed / ballController.airResistance);
        }

        public override void OnCollisionEnter(BallController ballController,
            BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            if (collision.gameObject.layer == ballController.groundLayer)
            {
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballMovingState);
            } else if (collision.gameObject.layer == ballController.hoppingWallLayer)
            {
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballWallHoppingState);
            }
        }

        public override void HandleInputAndConditions(BallController ballController,
            BallFiniteStateMachine ballFiniteStateMachine)
        {
            /*
             * The ball can only die when it is falling
             * If the ball is below the dieWhenBelowY threshold, the ball dies
             */
            if (!(ballController.transform.position.y < ballController.dieWhenBelowY)) return;
            
            ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballDeadState);
        }
    }
}