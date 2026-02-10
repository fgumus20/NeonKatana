using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI.States
{
    public class EnemyAttackState : EnemyState
    {
        public EnemyAttackState(EnemyController controller) : base(controller) { }

        public override void OnEnter()
        {
            Debug.Log("ATTACK: Vuruþ yapýldý!");
            controller.ChangeState(new EnemyRecoveryState(controller));
        }
    }

}
