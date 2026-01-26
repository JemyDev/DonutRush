using Components.Data;

namespace Components.StateMachine
{
    public abstract class State
    {
        protected readonly StateMachine StateMachine;
        protected readonly LevelParametersData LevelParameters;

        protected State(StateMachine stateMachine, LevelParametersData levelParameters)
        {
            StateMachine = stateMachine;
            LevelParameters = levelParameters;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }
}