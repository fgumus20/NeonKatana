using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "NeonKatana/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 3.5f;
    public float stoppingDistance = 1.5f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float anticipationDuration = 0.1f;
    public float recoveryDuration = 1.0f;
    public int damage = 1;

    public LayerMask playerLayer;
}