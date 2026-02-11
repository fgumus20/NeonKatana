using UnityEngine;
using System.Collections.Generic;

public class CombatController : MonoBehaviour
{
    [Header("--- Modules ---")]
    [SerializeField] private DashExecutor executor;

    private void OnEnable()
    {
        if (executor != null)
            executor.OnAttackComplete += HandleAttackComplete;
    }

    private void OnDisable()
    {
        if (executor != null)
            executor.OnAttackComplete -= HandleAttackComplete;
    }

    public void StartAttack(List<DashCommand> commands)
    {
        // Planning -> Attacking
        GameManager.Instance.ChangeState(GameState.Attacking);

        executor.Execute(commands);
    }

    private void HandleAttackComplete()
    {
        // Attacking -> Roaming
        GameManager.Instance.ChangeState(GameState.Roaming);
    }
}
