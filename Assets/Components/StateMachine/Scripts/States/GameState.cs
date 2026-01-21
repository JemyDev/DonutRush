using Services.GameEventService;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private int _life = 3;
        
        public GameState(StateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);
        }

        public override void Update() { }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
        }
        
        private void HandlePlayerCollision()
        {
            _life--;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_life);

            if (_life <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine));
            }
        }
    }
}