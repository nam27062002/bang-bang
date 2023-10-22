using System;
using InputSystem;
using UnityEngine;

namespace MovementSystem
{
    public class Player : MonoBehaviour
    {
        public Transform arrowPrefab;
        
        [NonSerialized] public PlayerInput PlayerInput;
        private PlayerMovementStateMachine _stateMachine;

        public float distance;
        private void Awake()
        {
            PlayerInput = GetComponent<PlayerInput>();
            _stateMachine = new PlayerMovementStateMachine(this);
            distance = Vector2.Distance(transform.position, arrowPrefab.position);
        }

        private void Start()
        {
            _stateMachine.ChangeState(StateMovement.PlayerIdlingState);
        }

        private void Update()
        {
            _stateMachine.HandleInput();
            _stateMachine.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine.PhysicsUpdate();
        }
    }
}