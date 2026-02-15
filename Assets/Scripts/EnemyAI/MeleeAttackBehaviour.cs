using UnityEngine;
using DG.Tweening;

namespace Scripts.EnemyAI
{
    public class MeleeAttackBehaviour : IAttackBehaviour
    {
        public void ExecuteAttack(EnemyController controller, EnemyBlackboard blackboard, EnemyAnimationManager animManager)
        {
            animManager.PlayAttack();

            PerformHitCheck(controller, blackboard);
        }


        private void PerformHitCheck(EnemyController controller, EnemyBlackboard blackboard)
        {
            Vector3 checkPos = controller.transform.position + controller.transform.forward * 1f;
            Collider[] hits = Physics.OverlapSphere(checkPos, 1f, blackboard.Data.playerLayer);

            foreach (var hit in hits)
            {
                Debug.Log("<color=red>Player get damage</color>");
            }
        }
    }
}