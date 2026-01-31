using Components.Data;
using Services.GameEventService;
using Services.SaveService;
using UnityEngine;

namespace Components.StateMachine.States
{
    public class GameState : State
    {
        private readonly LifeTracker _lifeTracker;
        private readonly ScoreTracker _scoreTracker;
        private readonly OrderTimerTracker _orderTimerTracker;
        private readonly LevelTracker _levelTracker;

        public GameState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData)
        {
            _lifeTracker = new LifeTracker(LevelParameters.PlayerLife);
            _scoreTracker = new ScoreTracker();
            _orderTimerTracker = new OrderTimerTracker(LevelParameters.OrderTimeLimit);
            _levelTracker = new LevelTracker(LevelParameters);
        }

        public override void Enter()
        {
            GameEventService.OnGameState?.Invoke(true);

            GameEventService.OnPlayerCollision += HandlePlayerCollision;
            GameEventService.OnOrderCompleted += HandleOrderCompleted;

            _lifeTracker.OnDeath += HandleDeath;
            _orderTimerTracker.OnTimerExpired += HandleTimerExpired;

            GameEventService.OnLevelStarted?.Invoke(_levelTracker.GetCurrentLevelParameters());
        }

        public override void Update()
        {
            _orderTimerTracker.Update(Time.deltaTime);
            GameEventService.OnTimerTick?.Invoke(_orderTimerTracker.RemainingTime);
        }

        public override void Exit()
        {
            GameEventService.OnGameState?.Invoke(false);
            
            GameEventService.OnPlayerCollision -= HandlePlayerCollision;
            GameEventService.OnOrderCompleted -= HandleOrderCompleted;
            
            _lifeTracker.OnDeath -= HandleDeath;
            _orderTimerTracker.OnTimerExpired -= HandleTimerExpired;
            
            ProgressService.SaveIfDirty();
        }
        
        private void HandlePlayerCollision()
        {
            _lifeTracker.LoseLife();
            GameEventService.OnPlayerLifeUpdated?.Invoke(_lifeTracker.CurrentLife);
        }

        private void HandleTimerExpired()
        {
            _lifeTracker.LoseLife();
            GameEventService.OnPlayerLifeUpdated?.Invoke(_lifeTracker.CurrentLife);
            GameEventService.OnOrderFailed?.Invoke();
        }

        private void HandleDeath()
        {
            UpdateHighScore();
            StateMachine.ChangeState(new GameOverState(StateMachine, LevelParameters));
        }
        
        private void HandleOrderCompleted(int score)
        {
            _scoreTracker.AddScore(score);
            _orderTimerTracker.Reset();
            
            var newParameters = _levelTracker.AdvanceLevel();

            GameEventService.OnScoreUpdated?.Invoke(_scoreTracker.CurrentScore);
            GameEventService.OnLevelChanged?.Invoke(newParameters);
        }

        private void UpdateHighScore()
        {
            ProgressService.RecordHighScore(_scoreTracker.CurrentScore);
        }
    }
}