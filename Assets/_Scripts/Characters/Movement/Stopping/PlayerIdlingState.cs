using UnityEngine;
using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerIdlingState : PlayerMovementState
    {
        
        public PlayerIdlingState(PlayerMovementStateMachine stateMachine) : base(stateMachine) { }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            MovementStateData.MovementSpeedModifier = 0f;
            ResetVelocity();
        }
        
        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerInput.Movement.started += OnMovementStarted;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerInput.Movement.started -= OnMovementStarted;
        }

        #endregion

        #region Input Methods
        
        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            StateMachine.ChangeState(StateMovement.PlayerRunningState);
        }
        #endregion
        
    }
}