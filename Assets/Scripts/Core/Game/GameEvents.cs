using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> EnemyDied;

    public static void RaiseEnemyDied(GameObject enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    public static event System.Action<int> EnemiesAliveChanged;
    public static void RaiseEnemiesAliveChanged(int alive) => EnemiesAliveChanged?.Invoke(alive);

    public static event Action<int, int, int> WaveStarted;
    public static void RaiseWaveStarted(int waveIndex, int totalWaves, int enemiesInWave)
        => WaveStarted?.Invoke(waveIndex, totalWaves, enemiesInWave);

    public static event System.Action GameOver;
    public static void RaiseGameOver() => GameOver?.Invoke();

    public static event Action<int, int> PlayerHpChanged;
    public static void RaisePlayerHpChanged(int current, int max)
        => PlayerHpChanged?.Invoke(current, max);

    public static event System.Action LevelCompleted;
    public static void RaiseLevelCompleted() => LevelCompleted?.Invoke();

}
