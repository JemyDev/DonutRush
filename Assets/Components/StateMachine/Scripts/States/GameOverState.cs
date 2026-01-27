using Components.Data;
using Services.GameEventService;

namespace Components.StateMachine.States
{
    public class GameOverState : State
    {
        public GameOverState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData) { }


        public override void Enter()
        {
            GameEventService.OnGameOverState?.Invoke(true);
        }

        public override void Update() { }

        public override void Exit()
        {
            GameEventService.OnGameOverState?.Invoke(false);
        }
    }
}