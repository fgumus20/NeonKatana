using Scripts.EnemyAI.States;
using System.Collections.Generic;

namespace Scripts.EnemyAI
{
    public class EnemyStateMachine
    {
        public EnemyState CurrentState { get; private set; }

        private readonly Dictionary<System.Type, EnemyState> _states = new();

        public void AddState(EnemyState state)
        {
            _states[state.GetType()] = state;
        }

        public void ChangeState<T>() where T : EnemyState
        {
            var type = typeof(T);
            if (!_states.ContainsKey(type)) return;

            CurrentState?.OnExit();
            CurrentState = _states[type];
            CurrentState?.OnEnter();
        }

        public void Update() => CurrentState?.OnUpdate();
    }
}