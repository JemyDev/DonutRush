using Components.Data;
using Components.SODatabase;
using Services.GameEventService;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private int _currentLife;
        private int _currentLevelIndex;

        public GameState(StateMachine stateMachine, LevelParametersData levelParametersData, int levelIndex = 1) : base(stateMachine, levelParametersData)
        {
            _currentLevelIndex = levelIndex;
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            GameEventService.OnOrderCompleted += HandleOrderCompleted;

            _currentLife = LevelParameters.PlayerLife;
        }

        public override void Update() { }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
            GameEventService.OnOrderCompleted -= HandleOrderCompleted;
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

        private void HandleOrderCompleted(int score)
        {
            _currentLevelIndex++;
            var newLevel = ScriptableObjectDatabase.Get<LevelParametersData>("Level" + _currentLevelIndex);

            if (newLevel is null)
                return;
            
            GameEventService.OnLevelChanged?.Invoke(newLevel);
        }
    }
}