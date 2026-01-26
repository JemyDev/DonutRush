using Components.Data;
using Services.GameEventService;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private int _currentLife;
        
        public GameState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData) { }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            
            _currentLife = LevelParameters.PlayerLife;
        }

        public override void Update() { }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
        }
        
        private void HandlePlayerCollision()
        {
            _currentLife--;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);

            if (_currentLife <= 0)
            {
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }
    }
}