using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    public class BallFiniteStateMachine
    {
        // Current State
        private BallBaseState _currentState;
        
        // Reference To Ball Controller
        private readonly BallController _ballController;

        // List Of States
        public readonly BallMovingState ballMovingState;
        public readonly BallBrakingState ballBrakingState;
        public readonly BallBouncingState ballBouncingState;
        public readonly BallWallHoppingState ballWallHoppingState;
        public readonly BallFallingState ballFallingState;
        public readonly BallDeadState ballDeadState;
        
        public BallFiniteStateMachine(BallController ballController)
        {
            _ballController = ballController;
            ballMovingState = new BallMovingState();
            ballBrakingState = new BallBrakingState();
            ballBouncingState = new BallBouncingState();
            ballWallHoppingState = new BallWallHoppingState();
            ballFallingState = new BallFallingState();
            ballDeadState = new BallDeadState();
        }

        public void Start()
        {
            ChangeState(ballFallingState);
        }
        
        public void Update()
        {
            _currentState.HandleInputAndConditions(_ballController, this);
        }
        
        public void FixedUpdate()
        {
            _currentState.FixedUpdate(_ballController, this);
        }
        
        public void OnCollisionEnter(Collision collision)
        {
            _currentState.OnCollisionEnter(_ballController, this, collision);
        }
        
        public void ChangeState(BallBaseState newState)
        {
            _currentState = newState;
            _currentState.Enter(_ballController, this);
        }
    }
}