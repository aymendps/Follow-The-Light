using Managers;
using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallDeadState : BallBaseState
    {
        public override void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            // Debug.Log("Entering Dead State");
            LevelManager.Instance.LoseLevel();
        }

        public override void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            
        }

        public override void OnCollisionEnter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine,
            Collision collision)
        {
            /*
             * The ball would collide with the ground after respawning
             * so we need to check if a collision with the ground has occurred
             * and if it has, we need to change the state to the moving state
             */
            if (collision.gameObject.layer == ballController.groundLayer)
            {
                ballFiniteStateMachine.ChangeState(ballFiniteStateMachine.ballMovingState);
            }
        }

        public override void HandleInputAndConditions(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine)
        {
            
        }
    }
}