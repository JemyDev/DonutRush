using Components.Data;
using Services.GameEventService;
using UnityEngine;

namespace Components.StateMachine.States
{
    public class CountdownState : State
    {
        private readonly int _levelIndex;
        private float _countdownTimer;

        public CountdownState(StateMachine stateMachine, LevelParametersData levelParametersData, int levelIndex = 1) : base(stateMachine, levelParametersData)
        {
            _levelIndex = levelIndex;
        }
        
        public override void Enter()
        {
            GameEventService.OnCountdownState?.Invoke(true);
            _countdownTimer = 3;
        }
        
        public override void Update()
        {
            _countdownTimer -= Time.deltaTime;

            if (_countdownTimer > 0)
            {
                GameEventService.OnCountdownTick?.Invoke(_countdownTimer);
                return;
            }
            
            StateMachine.ChangeState(new GameState(StateMachine, LevelParameters, _levelIndex));
        }
        
        public override void Exit()
        {
            GameEventService.OnCountdownState?.Invoke(false);
        }
    }
}