using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<GameObject> EnemyDied;

    public static void RaiseEnemyDied(GameObject enemy)
    {
        EnemyDied?.Invoke(enemy);
    }

    public static event Action<int, int, int> WaveStarted;
    public static void RaiseWaveStarted(int waveIndex, int totalWaves, int enemiesInWave)
        => WaveStarted?.Invoke(waveIndex, totalWaves, enemiesInWave);

    public static event System.Action GameOver;
    public static void RaiseGameOver() => GameOver?.Invoke();

    public static event Action<int, int> PlayerHpChanged;
    public static void RaisePlayerHpChanged(int current, int max)
        => PlayerHpChanged?.Invoke(current, max);


}
