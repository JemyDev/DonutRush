using UnityEngine;
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
            var baseParameters = ScriptableObjectDatabase.Get<LevelParametersData>("BaseLevelParameters");

            _stateMachine = new StateMachine();
            var initialState = new CountdownState(_stateMachine, baseParameters);

            _stateMachine.ChangeState(initialState);
        }

        public void Update() => _stateMachine.Update();
    }
}