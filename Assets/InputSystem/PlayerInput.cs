using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class PlayerInput : MonoBehaviour
    {
        public enum InputType
        {
            Movement,
            Dashing
        }
        private Dictionary<InputType, InputAction> _inputActions;
        private PlayerInputActions _playerInputActions;
        private PlayerInputActions.PlayerActions _playerActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerActions = _playerInputActions.Player;
            _inputActions = new Dictionary<InputType, InputAction>
            {
                { InputType.Movement, Movement },
                { InputType.Dashing, Dashing }
            };
        }
        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void OnDisable()
        {
            _playerActions.Disable();
        }
        

        
        #region API

        public InputAction Movement => _playerActions.Move;
        public InputAction Dashing => _playerActions.Dash;
        public Vector2 GetTargetPosition()
        {
            return Camera.main!.ScreenToWorldPoint(_playerActions.Mouse.ReadValue<Vector2>());
        }

        public void DisableInput(InputType type)
        {
            _inputActions[type].Disable();
        }

        public void EnableInput(InputType type)
        {
            _inputActions[type].Enable();
        }
        public Vector2 GetMovementDirection()
        {
            return _playerActions.Move.ReadValue<Vector2>();
        }
        #endregion
    }

}
