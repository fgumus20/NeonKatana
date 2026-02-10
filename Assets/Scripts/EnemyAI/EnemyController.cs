using Scripts.EnemyAI;
using UnityEngine;


namespace Scripts.EnemyAI
{
    public class EnemyController : MonoBehaviour
    {

        private EnemyState _currentState;
        private EnemyBlackboard _blackboard;

        public void Init(Transform target, EnemyDataSO data)
        {
            _blackboard = new EnemyBlackboard(target, data);

        }
        private void Awake()
        {
        }

        private void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(EnemyState next)
        {
            _currentState?.Exit();
            _currentState = next;
            _currentState?.Enter();
        }
    }
}
