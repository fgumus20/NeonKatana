using UnityEngine;

namespace Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private LevelDataSO currentLevelData; // Bölüm ayarlarý buradan okunur

        private int _currentWaveIndex = 0;

        private void Start() => StartNextWave();

        public void StartNextWave()
        {
            if (_currentWaveIndex < currentLevelData.waves.Count)
            {
                var waveInfo = currentLevelData.waves[_currentWaveIndex];
                waveManager.SpawnWave(waveInfo);
                _currentWaveIndex++;
            }
            else
            {
                Debug.Log("Bölüm Tamamlandý!");
            }
        }

        public void OnEnemyKilled()
        {
            // Sahadaki düþman sayýsýný kontrol et, 0 ise StartNextWave() çaðýr
        }
    }

}
