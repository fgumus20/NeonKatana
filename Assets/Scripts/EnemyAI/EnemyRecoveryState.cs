using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyRecoveryState : EnemyState
    {
        private float _timer;

        public EnemyRecoveryState(EnemyController controller, EnemyBlackboard enemyBlackboard) : base(controller, enemyBlackboard) { }

        public override void OnEnter()
        {
            _timer = enemyBlackboard.Data.recoveryDuration;
        }

        public override void OnUpdate()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
                controller.ChangeState(new EnemyChaseState(controller, enemyBlackboard));
        }

        public override void OnExit() {}

    }
}
