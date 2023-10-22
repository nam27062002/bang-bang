using System;
namespace MovementSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        private readonly PlayerIdlingState _idlingState;
        private readonly PlayerRunningState _runningState;
        private readonly PlayerDashingState _dashingState;
        
        public readonly PlayerMovementStateData MovementStateData;
        public readonly Player Player;
        
        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            MovementStateData = new PlayerMovementStateData();
            
            _idlingState = new PlayerIdlingState(this);
            _runningState = new PlayerRunningState(this);
            _dashingState = new PlayerDashingState(this);
        }
        
        public void ChangeState(StateMovement state)
        {
            switch (state)
            {
                case StateMovement.PlayerIdlingState:
                    ChangeState(_idlingState);
                    break;
                case StateMovement.PlayerRunningState:
                    ChangeState(_runningState);
                    break;
                case StateMovement.PlayerDashingState:
                    ChangeState(_dashingState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}