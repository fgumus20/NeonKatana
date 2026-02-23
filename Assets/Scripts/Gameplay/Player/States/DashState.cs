using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Scripts.EnemyAI;

namespace Scripts.Combat.States
{
    public class DashState : CombatState
    {

        private Collider[] _hitBuffer = new Collider[64];
        private readonly HashSet<int> _hitSet = new HashSet<int>(64);

        private float _nextScanTime;

        public DashState(CombatController CombatController, CombatBlackboard blackboard) : base(CombatController, blackboard) { }

        public override void OnEnter()
        {
            _hitSet.Clear();
            ExecuteDashSequence();
        }

        private void ExecuteDashSequence()
        {
            Sequence dashSequence = DOTween.Sequence();
            var commands = CombatBlackboard.DashPoints;
            int currentStep = 0;

            foreach (var cmd in commands)
            {
                int stepIndex = currentStep;
                float duration = (stepIndex % 2 == 0) ? 0.30f : 0.45f;

                Vector3 direction = (cmd.EndPos - cmd.StartPos).normalized;
                direction.y = 0;

                dashSequence.Append(CombatBlackboard.PlayerTransform.DORotateQuaternion(Quaternion.LookRotation(direction), .10f));

                dashSequence.AppendCallback(() =>
                {
                    CombatController.NotifyDashSegment(stepIndex);
                });

                dashSequence.Append(CombatBlackboard.PlayerRigidbody.DOMove(cmd.EndPos, duration).SetEase(Ease.Linear));

                currentStep++;
                dashSequence.AppendInterval(0.1f);
            }

            dashSequence.OnComplete(() =>
            {
                CombatController.NotifyDashEnded();
                GamePlayManager.Instance.ChangeState(GameState.Roaming);
            });
        }

        public override void Update() 
        {
            CheckHitDuringDash();
        }

        public override void OnExit() {
            CombatBlackboard.ClearPoints(); 
        }

        private void CheckHitDuringDash()
        {
            if (Time.time < _nextScanTime) return;
            _nextScanTime = Time.time + 0.03f;

            float scanRadius = CombatBlackboard.PlayerStats.sphereCastRadius;
            int count = Physics.OverlapSphereNonAlloc(
                CombatBlackboard.PlayerTransform.position + Vector3.up * 0.5f,
                scanRadius,
                _hitBuffer,
                CombatBlackboard.PlayerStats.enemyLayer
            );

            for (int i = 0; i < count; i++)
            {
                var enemy = _hitBuffer[i].GetComponent<EnemyController>();
                if (enemy != null && _hitSet.Add(enemy.GetInstanceID()))
                {
                    enemy.Die();

                    
                }
            }
        }
    }
}