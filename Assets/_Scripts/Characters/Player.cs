using System;
using InputSystem;
using UnityEngine;

namespace MovementSystem
{
    public class Player : MonoBehaviour
    {
        [Header("References")]
        public PlayerSo dataConfig;
        [Header("Model")]
        public Transform arrowPrefab;
        
        [NonSerialized] public Rigidbody2D Rigidbody;
        [NonSerialized] public PlayerInput PlayerInput;
        [NonSerialized] public float Distance;
        
        private PlayerMovementStateMachine _stateMachine; 
        
        private void Awake()
        {
            PlayerInput = GetComponent<PlayerInput>();
            Rigidbody = GetComponent<Rigidbody2D>();
            _stateMachine = new PlayerMovementStateMachine(this);
            Distance = Vector2.Distance(transform.position, arrowPrefab.position);
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