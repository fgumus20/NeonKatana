using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class DashExecutor : MonoBehaviour
{
    public event Action OnAttackComplete;
    private Rigidbody _rb;
    private Tween _moveTween;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Execute(List<DashCommand> commands)
    {
        Sequence attackSequence = DOTween.Sequence();

        foreach (var cmd in commands)
            attackSequence.Append(CreateDashTween(cmd));

        attackSequence.OnComplete(() =>
        {
            OnAttackComplete?.Invoke();
        });
    }

    private Tween CreateDashTween(DashCommand cmd)
    {
        float distance = Vector3.Distance(cmd.StartPos, cmd.EndPos);

        float speed = GetDashSpeed(cmd.Stats);
        float duration = distance / speed;
        if (duration < 0.01f) duration = 0.01f;

        _moveTween = _rb.DOMove(cmd.EndPos, duration);

        return _moveTween
            .SetEase(Ease.Linear)
            .OnStart(() =>
            {
                transform.LookAt(cmd.EndPos);
                CheckHits(cmd);
            });
    }

    private void CheckHits(DashCommand cmd)
    {
        if (cmd.Stats == null) return;

        float speed = GetDashSpeed(cmd.Stats);

        Vector3 direction = (cmd.EndPos - cmd.StartPos).normalized;
        float maxDistance = Vector3.Distance(cmd.StartPos, cmd.EndPos);

        RaycastHit[] hits = Physics.SphereCastAll(
            cmd.StartPos,
            cmd.Stats.sphereCastRadius,
            direction,
            maxDistance,
            cmd.Stats.enemyLayer
        );

        foreach (RaycastHit hit in hits)
        {
            SimpleEnemy enemy = hit.collider.GetComponent<SimpleEnemy>();
            if (enemy == null) continue;

            float timeToHit = hit.distance / speed;
            if (timeToHit < 0.05f) timeToHit = 0.05f;

            DOVirtual.DelayedCall(timeToHit, () =>
            {
                if (enemy != null) enemy.Die();
            });
        }
    }

    private float GetDashSpeed(PlayerStatsSO stats)
    {
        return (stats != null && stats.moveSpeed > 0f) ? stats.moveSpeed : 40f;
    }
}
