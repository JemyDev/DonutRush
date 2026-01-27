using Components.Data;
using Services.GameEventService;
using Services.SaveService;
using UnityEngine;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private int _currentLife;
        private int _currentLevel;
        private int _currentScore;
        private float _orderTimer;
        
        private const float MAX_SPEED = 15f;
        private const float SPEED_INCREMENT_PER_LEVEL = 0.75f;
        private const int MAX_INGREDIENTS_PER_ORDER = 3;
        private const int MAX_INGREDIENTS_PER_ORDER_LINE = 8;

        public GameState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData)
        {
            _currentLevel = 1;
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);
            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            GameEventService.OnOrderCompleted += HandleOrderCompleted;

            _currentLife = LevelParameters.PlayerLife;
            _orderTimer = LevelParameters.OrderTimeLimit;
        }

        public override void Update()
        {
            _orderTimer -= Time.deltaTime;

            if (_orderTimer > 0)
            {
                GameEventService.OnTimerTick?.Invoke(_orderTimer);
                return;
            }

            UpdateLife();
            GameEventService.OnOrderFailed?.Invoke();
            _orderTimer = LevelParameters.OrderTimeLimit;
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
            GameEventService.OnOrderCompleted -= HandleOrderCompleted;
        }

        private void HandlePlayerCollision()
        {
            UpdateLife();
        }

        private void UpdateLife()
        {
            _currentLife--;
            GameEventService.OnPlayerLifeUpdated?.Invoke(_currentLife);

            if (_currentLife <= 0)
            {
                SaveHighScore();
                StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
            }
        }

        private void HandleOrderCompleted(int score)
        {
            UpdateLevelParameters();
            _currentScore += score;
            _orderTimer = LevelParameters.OrderTimeLimit;
            GameEventService.OnScoreUpdated?.Invoke(_currentScore);
        }
        
        private void UpdateLevelParameters()
        {
            _currentLevel++;
            var newParameters = CalculateLevelParameters(_currentLevel);
            GameEventService.OnLevelChanged?.Invoke(newParameters);
        }

        private void SaveHighScore()
        {
            if (!SaveService.TryLoad(out var saveData))
            {
                saveData = new SaveData();
            }

            if (saveData.HighScore < _currentScore)
            {
                saveData.HighScore = _currentScore;
                SaveService.Save(saveData);
            }
        }

        private LevelParametersInfo CalculateLevelParameters(int level)
        {
            // Speed: linear growth per level
            var speed = LevelParameters.Speed + (level - 1) * SPEED_INCREMENT_PER_LEVEL;
            speed = Mathf.Min(speed, MAX_SPEED);

            // Max ingredients per order: +1 every 3 levels
            var maxIngredientsPerOrder = LevelParameters.MaxIngredientsPerOrder + (level - 1) / 3;
            maxIngredientsPerOrder = Mathf.Min(maxIngredientsPerOrder, MAX_INGREDIENTS_PER_ORDER);

            // Min ingredients per line: +1 every 4 levels
            var minIngredientsPerOrderLine = LevelParameters.MinIngredientsPerOrderLine + (level - 1) / 4;

            // Max ingredients per line: +1 every 2 levels
            var maxIngredientsPerOrderLine = LevelParameters.MaxIngredientsPerOrderLine + (level - 1) / 2;
            maxIngredientsPerOrderLine = Mathf.Min(maxIngredientsPerOrderLine, MAX_INGREDIENTS_PER_ORDER_LINE);

            // Ensure min <= max
            minIngredientsPerOrderLine = Mathf.Min(minIngredientsPerOrderLine, maxIngredientsPerOrderLine);

            return new LevelParametersInfo(
                level,
                speed,
                maxIngredientsPerOrder,
                minIngredientsPerOrderLine,
                maxIngredientsPerOrderLine
            );
        }
    }
}