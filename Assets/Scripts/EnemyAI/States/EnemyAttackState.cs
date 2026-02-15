using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyAttackState : EnemyState
    {
        private float _attackTimer;
        private readonly float _animationDuration = 1.0f;

        public EnemyAttackState(EnemyController controller, EnemyBlackboard enemyBlackboard) : base(controller, enemyBlackboard) { }

        public override void OnEnter()
        {
            controller.ExecuteAttack();
            _attackTimer = _animationDuration;
        }
        public override void OnUpdate() {

            _attackTimer -= Time.deltaTime;

            if (_attackTimer <= 0)
            {
                controller.ChangeState<EnemyRecoveryState>();
            }
        }

        public override void OnExit() {}


    }

}
