using Scripts.EnemyAI.States;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        public EnemyDataSO data;
        public Transform playerTransform;

        [HideInInspector] public NavMeshAgent agent;
        private EnemyState _currentState;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = data.moveSpeed;
            agent.stoppingDistance = data.stoppingDistance;
            agent.updateRotation = true;
        }

        private void Start()
        {
            ChangeState(new EnemyChaseState(this));
        }

        private void Update()
        {
            _currentState?.OnUpdate();
        }

        public void ChangeState(EnemyState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState?.OnEnter();
        }
    }

}