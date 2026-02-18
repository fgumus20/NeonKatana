using Scripts.EnemyAI;
using UnityEngine;

namespace Scripts.Level
{
    public class WaveManager : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public int SpawnWave(WaveData data)
        {
            int spawned = 0;
            foreach (var spawnInfo in data.enemiesInWave)
            {
                for (int i = 0; i < spawnInfo.count; i++)
                {
                    GameObject enemy = EnemyPool.Instance.GetEnemy();

                    Vector3 randomPos = data.spawnPositions[Random.Range(0, data.spawnPositions.Count)];

                    enemy.GetComponent<EnemyController>().ResetForSpawn(target, randomPos);
                    spawned++;
                }
            }
            return spawned;
        }
    }
        
}
