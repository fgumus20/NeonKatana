using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;

    private int enemiesAlive;

    private void OnEnable()
    {
        GameEvents.WaveStarted += OnWaveStarted;
        GameEvents.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        GameEvents.WaveStarted -= OnWaveStarted;
        GameEvents.EnemyDied -= OnEnemyDied;
    }

    private void OnWaveStarted(int waveIndex, int totalWaves, int enemiesInWave)
    {
        enemiesAlive = enemiesInWave;

        if (waveText) waveText.text = $"WAVE: {waveIndex}/{totalWaves}";
        if (enemiesText) enemiesText.text = $"ENEMIES: {enemiesAlive}";
    }

    private void OnEnemyDied(GameObject _)
    {
        enemiesAlive = Mathf.Max(0, enemiesAlive - 1);
        if (enemiesText) enemiesText.text = $"ENEMIES: {enemiesAlive}";
    }

}
