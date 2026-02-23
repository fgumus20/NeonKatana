using UnityEngine;


namespace Scripts.EnemyAI.States
{

    public class EnemyChaseState : EnemyState
    {

        private float _repathTimer;
        private const float RepathInterval = .5f;

        public EnemyChaseState(EnemyController controller, EnemyBlackboard enemyBlackboard) : base(controller, enemyBlackboard) { }

        public override void OnEnter()
        {
            float distance = Vector3.Distance(controller.transform.position, enemyBlackboard.Target.position);

            if (distance <= enemyBlackboard.Data.attackRange)
            {
                controller.ChangeState<EnemyAnticipationState>();
            } else
            {
                enemyBlackboard.Agent.isStopped = false;
                controller.PlayRun();
            }

        }

        public override void OnUpdate()
        {
            if (enemyBlackboard.Target == null) return;

            _repathTimer -= Time.deltaTime;
            if (_repathTimer <= 0f)
            {
                UpdatePath();
                _repathTimer = RepathInterval;
            }

            float distance = Vector3.Distance(controller.transform.position, enemyBlackboard.Target.position);

            if (distance <= enemyBlackboard.Data.attackRange)
            {
                controller.ChangeState<EnemyAnticipationState>();
            }
        }

        private void UpdatePath()
        {
            if (enemyBlackboard.Agent.isOnNavMesh)
            {
                enemyBlackboard.Agent.SetDestination(enemyBlackboard.Target.position);
            }
        }

        public override void OnExit()
        {
            if (enemyBlackboard.Agent.isOnNavMesh)
                enemyBlackboard.Agent.isStopped = true;
        }
    }



}
