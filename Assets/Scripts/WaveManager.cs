using Scripts.EnemyAI;
using UnityEngine;

namespace Scripts.Level
{
    public class WaveManager : MonoBehaviour
    {
        public void SpawnWave(WaveData data)
        {
            foreach (var spawnInfo in data.enemiesInWave)
            {
                for (int i = 0; i < spawnInfo.count; i++)
                {
                    GameObject enemy = EnemyPool.Instance.GetEnemy();

                    Vector3 randomPos = data.spawnPositions[Random.Range(0, data.spawnPositions.Count)];

                    enemy.GetComponent<EnemyController>().SetSpawnPoint(randomPos);
                }
            }
        }
    }
        
}
