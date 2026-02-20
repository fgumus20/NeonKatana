using Scripts.Player;
using UnityEngine;

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
            float radius = 1f;
            Collider[] hits = Physics.OverlapSphere(checkPos, radius, blackboard.Data.playerLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                var hp = hits[i].GetComponentInParent<PlayerHealth>();
                hp.TakeDamage(1);
                break;
            }
        }
    }
}
