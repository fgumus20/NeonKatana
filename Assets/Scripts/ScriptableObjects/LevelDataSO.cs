using UnityEngine;
using System.Collections.Generic;


namespace Scripts.Level
{
    [CreateAssetMenu(fileName = "NewLevelData", menuName = "NeonKatana/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        public List<WaveData> waves;
    }

    [System.Serializable]
    public struct WaveData
    {
        public string waveName;
        public List<EnemySpawnInfo> enemiesInWave;
        public List<Vector3> spawnPositions;
    }

    [System.Serializable]
    public struct EnemySpawnInfo
    {
        public string enemyType;
        public int count;
    }
}
