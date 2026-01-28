using Components.Data;
using Components.Managers;
using Services.GameEventService;
using Services.SaveService;
using UnityEngine;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private readonly LifeManager _lifeManager;
        private readonly ScoreManager _scoreManager;
        private readonly TimerManager _timerManager;
        private readonly LevelManager _levelManager;

        public GameState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData)
        {
            _lifeManager = new LifeManager(LevelParameters.PlayerLife);
            _scoreManager = new ScoreManager();
            _timerManager = new TimerManager(LevelParameters.OrderTimeLimit);
            _levelManager = new LevelManager(levelParametersData);
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);

            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            GameEventService.OnOrderCompleted += HandleOrderCompleted;

            _lifeManager.OnDeath += HandleDeath;
            _timerManager.OnTimerExpired += HandleTimerExpired;

            GameEventService.OnLevelStarted?.Invoke(_levelManager.GetCurrentLevelParameters());
        }

        public override void Update()
        {
            _timerManager.Update(Time.deltaTime);
            GameEventService.OnTimerTick?.Invoke(_timerManager.RemainingTime);
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
            GameEventService.OnOrderCompleted -= HandleOrderCompleted;
            
            _lifeManager.OnDeath -= HandleDeath;
            _timerManager.OnTimerExpired -= HandleTimerExpired;
        }
        
        private void HandlePlayerCollision()
        {
            _lifeManager.LoseLife();
            GameEventService.OnPlayerLifeUpdated?.Invoke(_lifeManager.CurrentLife);
        }

        private void HandleTimerExpired()
        {
            _lifeManager.LoseLife();
            GameEventService.OnPlayerLifeUpdated?.Invoke(_lifeManager.CurrentLife);
            GameEventService.OnOrderFailed?.Invoke();
        }

        private void HandleDeath()
        {
            SaveHighScore();
            StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
        }
        
        private void HandleOrderCompleted(int score)
        {
            _scoreManager.AddScore(score);
            _timerManager.Reset();
            
            var newParameters = _levelManager.AdvanceLevel();

            GameEventService.OnScoreUpdated?.Invoke(_scoreManager.CurrentScore);
            GameEventService.OnLevelChanged?.Invoke(newParameters);
        }

        private void SaveHighScore()
        {
            if (!SaveService.TryLoad(out var saveData))
            {
                saveData = new SaveData();
            }

            if (saveData.HighScore < _scoreManager.CurrentScore)
            {
                saveData.HighScore = _scoreManager.CurrentScore;
                SaveService.Save(saveData);
            }
        }
    }
}