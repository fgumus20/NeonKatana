using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace Scripts.Combat.States
{
    public class DashState : CombatState
    {

        private Collider[] _hitBuffer = new Collider[64];
        private readonly HashSet<int> _hitSet = new HashSet<int>(64);
        public DashState(CombatController controller, CombatBlackboard blackboard) : base(controller, blackboard) { }

        public override void OnEnter()
        {
            ExecuteDashSequence();
        }

        private void ExecuteDashSequence()
        {
            Sequence dashSequence = DOTween.Sequence();
            var commands = CombatBlackboard.DashPoints;
            int currentStep = 0;

            foreach (var cmd in commands)
            {
                float distance = Vector3.Distance(cmd.StartPos, cmd.EndPos);
                //float duration = distance / CombatBlackboard.PlayerStats.dashMoveSpeed;
                float duration = .45f;
                int indexToSend = currentStep % 2;

                Vector3 direction = (cmd.EndPos - cmd.StartPos).normalized;

                direction.y = 0;
                dashSequence.Append(CombatBlackboard.PlayerTransform.DORotateQuaternion(Quaternion.LookRotation(direction), .05f));
                dashSequence.AppendCallback(() =>
                {
                    CombatBlackboard.PlayerAnimator.SetInteger("animIndex", indexToSend);
                    CombatBlackboard.PlayerAnimator.SetTrigger("attack");

                });



                dashSequence.Append(CombatBlackboard.PlayerRigidbody.DOMove(cmd.EndPos, duration).SetEase(Ease.Linear));

                dashSequence.AppendCallback(() =>
                {
                    ApplyHitEffects(cmd);
                });
                currentStep++;
                dashSequence.AppendInterval(0.1f);
            }

            dashSequence.OnComplete(() =>
            {
                GameManager.Instance.ChangeState(GameState.Roaming);
            });
        }

        private void ApplyHitEffects(DashCommand cmd)
        {
            _hitSet.Clear();

            float r = CombatBlackboard.PlayerStats.sphereCastRadius;
            Vector3 a = cmd.StartPos + Vector3.up * 0.5f;
            Vector3 b = cmd.EndPos + Vector3.up * 0.5f;

            int count = Physics.OverlapCapsuleNonAlloc(
                a, b, r,
                _hitBuffer,
                CombatBlackboard.PlayerStats.enemyLayer,
                QueryTriggerInteraction.Ignore
            );

            for (int i = 0; i < count; i++)
            {
                var col = _hitBuffer[i];
                if (col == null) continue;

                int id = col.GetInstanceID();
                if (!_hitSet.Add(id)) continue;

                if (col.TryGetComponent(out SimpleEnemy enemy))
                    enemy.Die();
            }
        }


        public override void Update() { }
        public override void OnExit() {

            CombatBlackboard.ClearPoints(); }
    }
}