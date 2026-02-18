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

    #if UNITY_EDITOR
            private void DrawDebugCircle(Vector3 center, Vector3 normal, float radius, int segments, float duration)
            {
                // normal'e göre bir düzlemde daire çizmek için 2 eksen bul
                Vector3 axisA = Vector3.Cross(normal, Vector3.right);
                if (axisA.sqrMagnitude < 0.001f) axisA = Vector3.Cross(normal, Vector3.forward);
                axisA.Normalize();

                Vector3 axisB = Vector3.Cross(normal, axisA).normalized;

                float step = 360f / segments;
                Vector3 prev = center + (axisA * radius);

                for (int i = 1; i <= segments; i++)
                {
                    float rad = Mathf.Deg2Rad * (step * i);
                    Vector3 next = center + (axisA * Mathf.Cos(rad) + axisB * Mathf.Sin(rad)) * radius;
                    Debug.DrawLine(prev, next, Color.red, duration);
                    prev = next;
                }
            }
    #endif  

        private void PerformHitCheck(EnemyController controller, EnemyBlackboard blackboard)
        {
            Vector3 checkPos = controller.transform.position + controller.transform.forward * 1f;
            float radius = 1f;
        #if UNITY_EDITOR
            DrawDebugCircle(checkPos, Vector3.up, radius, 24, 0.2f);
        #endif  

            Collider[] hits = Physics.OverlapSphere(checkPos, radius, blackboard.Data.playerLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                var hp = hits[i].GetComponentInParent<PlayerHealth>();
                hp.TakeDamage(1);
                return;
            }
        }
    }
}
