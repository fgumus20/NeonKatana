using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "NeonKatana/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("--- Movement Settings ---")]
    public int dashMoveCount = 3;
    public float minDashDistance = 3f;
    public float maxDashDistance = 6f;
    public float dashMoveSpeed = 40f;

    [Header("--- Physic ---")]
    public float sphereCastRadius = 3f;
    public LayerMask obstacleLayer;
    public LayerMask enemyLayer;
}