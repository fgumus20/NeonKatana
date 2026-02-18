using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> EnemyDied;

    public static void RaiseEnemyDied(GameObject enemy)
    {
        EnemyDied?.Invoke(enemy);
    }
}
