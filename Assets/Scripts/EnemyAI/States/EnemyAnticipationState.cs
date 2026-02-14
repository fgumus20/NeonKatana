using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyAnticipationState : EnemyState
    {
        private float _timer;

        public EnemyAnticipationState(EnemyController controller, EnemyBlackboard enemyBlackboard) : base(controller, enemyBlackboard) { }

        public override void OnEnter()
        {
            enemyBlackboard.Agent.isStopped = true;
            _timer = enemyBlackboard.Data.anticipationDuration;
        }

        public override void OnUpdate()
        {
            _timer -= Time.deltaTime;

            // Düþman dururken oyuncuya dönmeye devam etmeli (Tracking)
            Vector3 dir = (enemyBlackboard.Target.position - controller.transform.position).normalized;
            dir.y = 0;
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir);
                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, targetRot, Time.deltaTime * 10f);
            }

            if (_timer <= 0)
                controller.ChangeState<EnemyAttackState>();
        }

        public override void OnExit() {}
    }

}