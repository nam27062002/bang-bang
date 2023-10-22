using System;
namespace MovementSystem
{
    public class PlayerMovementStateMachine : StateMachine
    {
        private readonly PlayerIdlingState _idlingState;
        private readonly PlayerRunningState _runningState;
        public Player Player;
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        public PlayerMovementStateMachine(Player player)
        {
            Player = player;
            _idlingState = new PlayerIdlingState(this);
            _runningState = new PlayerRunningState(this);
        }
    }
}