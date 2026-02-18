using UnityEngine;

namespace Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private LevelDataSO currentLevelData;

        private int _currentWaveIndex = 0;

        private int _enemiesAlive;

        private void Start() 
        {
            StartNextWave();
        } 

        private void OnEnable()
        {
            GameEvents.EnemyDied += HandleEnemyDied;
        }

        private void OnDisable()
        {
            GameEvents.EnemyDied -= HandleEnemyDied;
        }

        private void HandleEnemyDied(GameObject enemy)
        {
            _enemiesAlive--;
            if (_enemiesAlive <= 0)
                StartNextWave();
        }
        

        public void StartNextWave()
        {
            if (_currentWaveIndex < currentLevelData.waves.Count)
            {
                var waveInfo = currentLevelData.waves[_currentWaveIndex];
                _enemiesAlive = waveManager.SpawnWave(waveInfo);
                _currentWaveIndex++;
            }
            else
            {
                Debug.Log("Level Completed!");
            }
        }
    }

}
