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
        GameEvents.EnemiesAliveChanged += OnEnemiesAliveChanged;
    }

    private void OnDisable()
    {
        GameEvents.WaveStarted -= OnWaveStarted;
        GameEvents.EnemiesAliveChanged -= OnEnemiesAliveChanged;
    }

    private void OnWaveStarted(int wave, int total, int enemiesAlive)
    {
        waveText.text = $"WAVE: {wave}/{total}";
        enemiesText.text = $"ENEMIES: {enemiesAlive}";
    }

    private void OnEnemiesAliveChanged(int alive)
    {
        enemiesText.text = $"ENEMIES: {alive}";
    }

}
