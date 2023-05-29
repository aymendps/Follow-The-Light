using UnityEngine;

namespace Gameplay.Ball_Finite_State_Machine
{
    /// <summary>
    /// Base State Class For Ball Finite State Machine
    /// </summary>
    public abstract class BallBaseState
    {
        public abstract void Enter(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine);
        public abstract void FixedUpdate(BallController ballController, BallFiniteStateMachine ballFiniteStateMachine);
        public abstract void OnCollisionEnter(BallController ballController,
            BallFiniteStateMachine ballFiniteStateMachine, Collision collision);
        public abstract void HandleInputAndConditions(BallController ballController,
            BallFiniteStateMachine ballFiniteStateMachine);
    }
}