using UnityEngine.InputSystem;

namespace MovementSystem
{
    public class PlayerRunningState : PlayerMovingState
    {
        private readonly PlayerRunningConfig _runningConfig;

        public PlayerRunningState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            _runningConfig = MovementConfig.runningConfig;
        }
        
        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            MovementStateData.MovementSpeedModifier = _runningConfig.speedModifier;
        }
        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();
            PlayerInput.Movement.canceled += OnMovementCanceled;
        }
        
        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();
            PlayerInput.Movement.canceled -= OnMovementCanceled;
        }
        
        #endregion

        #region Input Methods
        private void OnMovementCanceled(InputAction.CallbackContext context)
        {
            StateMachine.ChangeState(StateMovement.PlayerIdlingState);
        }
        #endregion
    }
}