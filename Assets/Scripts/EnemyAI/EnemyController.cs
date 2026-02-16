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
        [SerializeField] private Transform playerTransform;

        private NavMeshAgent agent;
        private EnemyBlackboard enemyBlackboard;
        private EnemyAnimationManager animationManager;
        IAttackBehaviour attackBehaviour;

        private EnemyStateMachine _stateMachine;

        private EnemyVfxController _vfxController;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = data.moveSpeed;
            agent.stoppingDistance = data.stoppingDistance;
            agent.updateRotation = true;

            
            animationManager = GetComponent<EnemyAnimationManager>();

            attackBehaviour = new MeleeAttackBehaviour();
            enemyBlackboard = new EnemyBlackboard(playerTransform, data, agent);

            _vfxController = GetComponent<EnemyVfxController>();
            CreateStates();
        }

        private void Start()
        {

            _stateMachine.ChangeState<EnemyChaseState>();
        }

        private void Update()
        {
            _stateMachine.Update();
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


        public void ExecuteAttack() {
            attackBehaviour.ExecuteAttack(this, enemyBlackboard, animationManager);
        }

        public void PlayRun()
        {
            animationManager.PlayRun();
        }

        public void Die()
        {
         
            GetComponent<Collider>().enabled = false;
            _vfxController.PlayDeathEffect();
            transform.DOKill();
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
            {
                Destroy(gameObject);
            });

            //Debug.Log("Enemy died: " + gameObject.name);
        }
    }

}