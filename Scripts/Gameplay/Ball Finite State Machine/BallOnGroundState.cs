using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public abstract class BallOnGroundState : BallBaseState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering On Ground State");
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
        }

        public override void HandleInputAndConditions(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            if (ballController.AttemptingToBounce)
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballBouncingState);
            else if(!ballController.IsGrounded())
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballFallingState);
        }
    }
}