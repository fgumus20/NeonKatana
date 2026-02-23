using System.Collections;
using UnityEngine;

namespace Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        [SerializeField] private WaveManager waveManager;
        [SerializeField] private LevelDataSO currentLevelData;

        [Header("Transitions")]
        [SerializeField] private float nextWaveDelay = 1f;
        [SerializeField] private float levelCompleteDelay = 0.7f;

        private int _currentWaveIndex = 0;
        private int _enemiesAlive;
        private bool _transitioning;

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            GameEvents.EnemyDied += HandleEnemyDied;
        }

        private void OnDisable()
        {
            GameEvents.EnemyDied -= HandleEnemyDied;
        }

        public void BeginLevel()
        {
            _transitioning = false;
            _currentWaveIndex = 0;
            StartNextWave();
        }

        private void HandleEnemyDied(GameObject enemy)
        {
            if (_transitioning) return;

            _enemiesAlive--;
            GameEvents.RaiseEnemiesAliveChanged(_enemiesAlive);

            if (_enemiesAlive <= 0)
            {
                _transitioning = true;
                StartCoroutine(StartNextWaveAfterDelay(nextWaveDelay));
            }


        }

        private IEnumerator StartNextWaveAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            _transitioning = false;
            StartNextWave();
        }

        public void StartNextWave()
        {
            if (_currentWaveIndex < currentLevelData.waves.Count)
            {
                var waveInfo = currentLevelData.waves[_currentWaveIndex];
                _enemiesAlive = waveManager.SpawnWave(waveInfo);

                int waveNumber = _currentWaveIndex + 1;
                int totalWaves = currentLevelData.waves.Count;

                GameEvents.RaiseWaveStarted(waveNumber, totalWaves, _enemiesAlive);

                _currentWaveIndex++;
            }
            else
            {
                StartCoroutine(LevelCompletedAfterDelay(levelCompleteDelay));
            }
        }

        private IEnumerator LevelCompletedAfterDelay(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            Debug.Log("Level Completed!");
            GameEvents.RaiseLevelCompleted();
        }
    }
}