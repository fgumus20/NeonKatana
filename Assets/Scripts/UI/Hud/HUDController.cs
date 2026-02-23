using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text waveText;
    [SerializeField] private TMP_Text enemiesText;
    [SerializeField] private WaveBannerController waveBanner;

    private void OnEnable()
    {
        GameEvents.WaveStarted += OnWaveStarted;
        GameEvents.EnemiesAliveChanged += OnEnemiesAliveChanged;
    }

    private void OnDisable()
    {
        GameEvents.WaveStarted -= OnWaveStarted;
        GameEvents.EnemiesAliveChanged -= OnEnemiesAliveChanged;
    }

    private void OnWaveStarted(int waveIndex, int totalWaves, int enemiesAlive)
    {
        waveText.text = $"WAVE: {waveIndex}/{totalWaves}";
        enemiesText.text = $"ENEMIES: {enemiesAlive}";
        waveBanner.ShowWave(waveIndex, totalWaves);
    }

    private void OnEnemiesAliveChanged(int alive)
    {
        enemiesText.text = $"ENEMIES: {alive}";
    }

}
