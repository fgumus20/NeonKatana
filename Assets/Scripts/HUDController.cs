using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private TMP_Text hpText;

    private int enemiesAlive;

    private void OnEnable()
    {
        GameEvents.WaveStarted += OnWaveStarted;
        GameEvents.EnemyDied += OnEnemyDied;
        GameEvents.PlayerHpChanged += OnHpChanged;
    }

    private void OnDisable()
    {
        GameEvents.WaveStarted -= OnWaveStarted;
        GameEvents.EnemyDied -= OnEnemyDied;
        GameEvents.PlayerHpChanged -= OnHpChanged;
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

    private void OnHpChanged(int current, int max)
    {
        if (hpText) hpText.text = $"HP: {current}";
    }
}
