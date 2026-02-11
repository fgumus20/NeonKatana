using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "NeonKatana/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("--- Movement Settings ---")]
    public int dashMoveCount = 3;
    public float maxDashDistance = 6f;
    public float dashMoveSpeed = 40f;

    [Header("--- Physic ---")]
    public float sphereCastRadius = 1f;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;
}