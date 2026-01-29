using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "NeonBlade/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("--- Movement Settings ---")]
    public int maxMoveCount = 3;
    public float maxDashDistance = 6f;
    public float moveSpeed = 40f;

    [Header("--- Physic ---")]
    public float sphereCastRadius = 1f;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;
}