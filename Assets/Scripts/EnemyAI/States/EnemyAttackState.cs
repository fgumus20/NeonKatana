using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyController controller, EnemyBlackboard enemyBlackboard, EnemyAnimationManager animationManager) 
            : base(controller, enemyBlackboard, animationManager) { }

        public override void OnEnter()
        {
            animationManager.PlayAttack();
            //controller.ChangeState<EnemyRecoveryState>();
        }
        public override void OnUpdate() {}

        public override void OnExit() {}


    }

}
