using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyController controller, EnemyBlackboard enemyBlackboard) : base(controller, enemyBlackboard) { }

        public override void OnEnter()
        {
            controller.ChangeState(new EnemyRecoveryState(controller,enemyBlackboard));
        }
        public override void OnUpdate() {}

        public override void OnExit() {}


    }

}
