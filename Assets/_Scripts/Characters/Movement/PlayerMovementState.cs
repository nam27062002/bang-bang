using UnityEngine;
using Utilities;
using InputSystem;

namespace MovementSystem
{
    public class PlayerMovementState : IState
    {
        private readonly Player _player;
        private readonly PlayerInput _playerInput;
        protected PlayerMovementState(PlayerMovementStateMachine stateMachine)
        {
            _player = stateMachine.Player;
            _playerInput = _player.PlayerInput;
        }
        
        #region IState Methods
        public void Enter()
        {
            Debug.Log($"State: {GetType().Name}");
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            HandleLookAtTarget();
        }

        public void Update()
        {
        }

        public void PhysicsUpdate()
        {
        }

        public void OnAnimationEnterEvent()
        {
        }

        public void OnAnimationExitEvent()
        {
        }

        public void OnAnimationTransitionEvent()
        {
        }
        
        #endregion 
        
        #region Main Methods

        #region Player Target Handling
        
        private void HandleLookAtTarget()
        {
            Vector2 unit = GetCharacterLookDirection();
            SetDirectionLookAt(unit);
            SetPositionLookAt(unit);
        }
        
        private Vector2 GetCharacterLookDirection()
        {
            Vector2 currentPosition = _player.transform.position;
            Vector2 targetPosition = GetPlayerLookAtPosition();
            return VectorUtilities.GetUnitDirection(currentPosition, targetPosition);
        }
        
        private Vector2 GetPlayerLookAtPosition()
        {
            return _playerInput.GetTargetPosition();
        }

        private void SetDirectionLookAt(Vector2 unit)
        {
            _player.arrowPrefab.up = unit;
        }
        
        private void SetPositionLookAt(Vector2 unit)
        {
            _player.arrowPrefab.position = _player.transform.position + _player.distance * new Vector3(unit.x,unit.y,0);
        }
        
        #endregion
       
        
        #endregion
    }
}