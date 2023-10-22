namespace MovementSystem
{
    public abstract class StateMachine
    {
        private IState _currentState;

        protected void ChangeState(IState state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void HandleInput()
        {
            _currentState?.HandleInput();
        }

        public void Update()
        {
            _currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            _currentState?.PhysicsUpdate();
        }
        
        protected void OnAnimationEnterEvent()
        {
            _currentState?.OnAnimationEnterEvent();
        }

        protected void OnAnimationExitEvent()
        {
            _currentState?.OnAnimationExitEvent();
        }

        protected void OnAnimationTransitionEvent()
        {
            _currentState?.OnAnimationTransitionEvent();
        }
    }
}