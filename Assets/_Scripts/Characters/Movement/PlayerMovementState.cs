using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;
using PlayerInput = InputSystem.PlayerInput;

namespace MovementSystem
{
    public class PlayerMovementState : IState
    {
        private readonly Player _player;
        protected readonly Rigidbody2D Rigidbody;
        protected readonly PlayerMovementStateMachine StateMachine;
        protected readonly PlayerInput PlayerInput;
        protected readonly PlayerMovementStateData MovementStateData;
        protected readonly PlayerMovementConfig MovementConfig;
        protected PlayerMovementState(PlayerMovementStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            _player = stateMachine.Player;
            PlayerInput = _player.PlayerInput;
            Rigidbody = _player.Rigidbody;
            MovementStateData = stateMachine.MovementStateData;
            MovementConfig = _player.dataConfig.movementConfig;
        }
        
        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log($"State: {GetType().Name}");
            AddInputActionsCallbacks();
        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }

        public virtual void HandleInput()
        {
            GetMovementInput();
            HandleLookAtTarget();
        }

        public virtual void Update()
        {
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void OnAnimationEnterEvent()
        {
        }

        public virtual void OnAnimationExitEvent()
        {
        }

        public virtual void OnAnimationTransitionEvent()
        {
        }
        
        #endregion 
        
        #region Main Methods

        #region Target Handling Methods
        
        private void HandleLookAtTarget()
        {
            Vector2 unit = GetCharacterLookDirection();
            SetDirectionLookAt(unit);
            SetPositionLookAt(unit);
        }
        

        
        private Vector2 GetPlayerLookAtPosition()
        {
            return PlayerInput.GetTargetPosition();
        }

        private void SetDirectionLookAt(Vector2 unit)
        {
            _player.arrowPrefab.up = unit;
        }
        
        private void SetPositionLookAt(Vector2 unit)
        {
            _player.arrowPrefab.position = _player.transform.position + _player.Distance * new Vector3(unit.x,unit.y,0);
        }
        
        #endregion

        #region Move Methods

        private void GetMovementInput()
        {
            MovementStateData.MovementInput = PlayerInput.GetMovementDirection();
        }
        
        private void Move()
        {
            if (!CanMove()) return;
            Vector2 movementDirection = MovementStateData.MovementInput;
            float movementSpeed = GetMovementSpeed();
            Vector2 currentPlayerHorizontalVelocity = Rigidbody.velocity;
            Rigidbody.AddForce(movementDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode2D.Impulse);
        }

        private bool CanMove()
        {
            return IsMovementPerformed() && 
                   MovementStateData.MovementSpeedModifier != 0;
        }
        
        #endregion
        
        #endregion

        #region Reusable Methods
        
        protected void ResetVelocity()
        {
            Rigidbody.velocity = Vector2.zero;
        }

        protected bool IsMovementPerformed()
        {
            return MovementStateData.MovementInput != Vector2.zero;
        }
        protected Vector2 GetCharacterLookDirection()
        {
            Vector2 currentPosition = _player.transform.position;
            Vector2 targetPosition = GetPlayerLookAtPosition();
            return VectorUtilities.GetUnitDirection(currentPosition, targetPosition);
        }
        protected float GetMovementSpeed()
        {
            return MovementConfig.baseSpeed * MovementStateData.MovementSpeedModifier;
        }
        
        protected virtual void AddInputActionsCallbacks()
        {
            PlayerInput.Dashing.started += OnDashingStarted;
        }
        
        protected virtual void RemoveInputActionsCallbacks()
        {
            PlayerInput.Dashing.started -= OnDashingStarted;
        }
        #endregion
        

        #region Input Methods
        
        private void OnDashingStarted(InputAction.CallbackContext context)
        {
            StateMachine.ChangeState(StateMovement.PlayerDashingState);
        }
        
        #endregion


    }
}