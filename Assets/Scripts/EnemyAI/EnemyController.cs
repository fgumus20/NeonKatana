using DG.Tweening;
using Scripts.EnemyAI.States;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO data;
        [SerializeField] private Transform playerTransform;

        private NavMeshAgent agent;
        private EnemyState _currentState;
        private EnemyBlackboard enemyBlackboard;
        

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = data.moveSpeed;
            agent.stoppingDistance = data.stoppingDistance;
            agent.updateRotation = true;

            enemyBlackboard = new EnemyBlackboard(playerTransform, data, agent);

        }

        private void Start()
        {
            ChangeState(new EnemyChaseState(this, enemyBlackboard));
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

        public void Die()
        {
            GetComponent<Collider>().enabled = false;

            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                Destroy(gameObject);
            });

            Debug.Log("Enemy died: " + gameObject.name);
        }
    }

}