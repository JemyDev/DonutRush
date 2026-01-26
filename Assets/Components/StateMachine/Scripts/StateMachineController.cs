using UnityEngine;
using Services.SaveService;
using Components.Data;
using Components.SODatabase;
using Components.StateMachine.States;

namespace Components.StateMachine
{
    public class StateMachineController : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Start()
        {
            var levelIndex = 1;
            if (SaveService.TryLoad(out var saveData))
            {
                levelIndex = saveData.LevelIndex;
            }
            
            var parameters = ScriptableObjectDatabase.Get<LevelParametersData>("Level" + levelIndex);
            
            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, parameters);
            
            _stateMachine.ChangeState(initialState);
        }
        
        public void Update() => _stateMachine.Update();
    }
}