using DG.Tweening;
using Scripts.EnemyAI.States;
using Scripts.EnemyAI.Vfx;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyDataSO data;
        private Transform playerTransform;

        private NavMeshAgent agent;
        private EnemyBlackboard enemyBlackboard;
        private EnemyAnimationManager animationManager;
        private IAttackBehaviour attackBehaviour;
        private EnemyStateMachine _stateMachine;
        private Collider _collider;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _collider = GetComponent<Collider>();
            animationManager = GetComponent<EnemyAnimationManager>();

            agent.speed = data.moveSpeed;
            agent.stoppingDistance = data.stoppingDistance;
            agent.updateRotation = true;

            attackBehaviour = new MeleeAttackBehaviour();
            enemyBlackboard = new EnemyBlackboard(null, data, agent);

            CreateStates();
        }

        private void OnEnable()
        {
            transform.DOKill();
            transform.localScale = Vector3.one;

            if (_collider != null) _collider.enabled = true;

            if (agent != null)
            {
                agent.enabled = true;
            }
        }

        private void Update()
        {
            _stateMachine?.Update();
        }

        public void ChangeState<T>() where T : EnemyState => _stateMachine.ChangeState<T>();

        private void CreateStates()
        {
            _stateMachine = new EnemyStateMachine();

            _stateMachine.AddState(new EnemyChaseState(this, enemyBlackboard));
            _stateMachine.AddState(new EnemyAnticipationState(this, enemyBlackboard));
            _stateMachine.AddState(new EnemyAttackState(this, enemyBlackboard));
            _stateMachine.AddState(new EnemyRecoveryState(this, enemyBlackboard));
        }

        public void ExecuteAttack()
        {
            attackBehaviour.ExecuteAttack(this, enemyBlackboard, animationManager);
        }

        public void PlayRun()
        {
            animationManager.PlayRun();
        }

        public void ResetForSpawn(Transform player, Vector3 position)
        {
            playerTransform = player;
            enemyBlackboard.SetTarget(playerTransform);
            agent.Warp(position);

            _stateMachine.ChangeState<EnemyChaseState>();
        }


        public void Die()
        {
            if (_collider != null) _collider.enabled = false;
 
            GameEvents.RaiseEnemyDied(gameObject);

            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                EnemyPool.Instance.ReturnEnemy(gameObject);
            });
        }
    }
}
