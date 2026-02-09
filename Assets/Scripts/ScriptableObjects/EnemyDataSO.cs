using UnityEngine;

[CreateAssetMenu(menuName = "NeonKatana/Enemy Data", fileName = "EnemyData_")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Core")]
    public int hp = 1;
    public float moveSpeed = 4f;

    [Header("Attack")]
    public float attackRange = 1.2f;
    public int damage = 1;
    public float windupTime = 0.25f;
    public float recoveryTime = 0.15f;
    public float cooldown = 0.6f;
}
