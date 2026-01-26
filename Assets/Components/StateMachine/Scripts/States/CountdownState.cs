using Components.Data;
using Services.GameEventService;
using UnityEngine;

namespace Components.StateMachine.States
{
    public class CountdownState : State
    {
        private float _countdownTimer;
        
        public CountdownState(StateMachine stateMachine, LevelParametersData levelParametersData) : base(stateMachine, levelParametersData) { }
        
        public override void Enter()
        {
            Services.GameEventService.GameEventService.OnCountdownState?.Invoke(true);
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
            
            StateMachine.ChangeState(new GameState(StateMachine, LevelParameters));
        }
        
        public override void Exit()
        {
            Services.GameEventService.GameEventService.OnCountdownState?.Invoke(false);
        }
    }
}