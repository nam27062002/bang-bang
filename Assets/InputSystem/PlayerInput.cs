using UnityEngine;
namespace InputSystem
{
    public class PlayerInput : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        private PlayerInputActions.PlayerActions _playerActions;
        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _playerActions = _playerInputActions.Player;
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

        public Vector2 GetTargetPosition()
        {
            return Camera.main!.ScreenToWorldPoint(_playerActions.Mouse.ReadValue<Vector2>());
        }
        #endregion
    }

}
