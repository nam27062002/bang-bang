using InputSystem;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerDashingState : PlayerMovingState
    {
        private readonly PlayerDashingConfig _dashingConfig;
        private int _consecutiveDashesUsed;
        private float _timeStarted;
        public PlayerDashingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            _dashingConfig = MovementConfig.dashingConfig;
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();
            if (!CanDash()) return;
            StartDashing();
        }

        public override void Update()
        {
            base.Update();
            if (Time.time < _timeStarted + _dashingConfig.dashTime) return;
            StopDashing();
        }

        #endregion

        #region Main Methods

        private bool CanDash()
        {
            return Time.time >= _timeStarted + _dashingConfig.countdown;
        }

        private void StartDashing()
        {
            _timeStarted = Time.time;
            PlayerInput.DisableInput(PlayerInput.InputType.Movement);
            MovementStateData.MovementSpeedModifier = _dashingConfig.speedModifier;
            AddForceOnTransitionFromStationaryState();
        }
        
        
        
        
        private void AddForceOnTransitionFromStationaryState()
        {
            Vector3 characterDirection = GetCharacterLookDirection();
            Rigidbody.velocity = characterDirection * GetMovementSpeed();
        }

        private void StopDashing()
        {
            PlayerInput.EnableInput(PlayerInput.InputType.Movement);
            StateMovement state = IsMovementPerformed()
                ? StateMovement.PlayerRunningState
                : StateMovement.PlayerIdlingState;
            StateMachine.ChangeState(state);
        }

        #endregion
    }
}