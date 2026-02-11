using UnityEngine;

namespace Scripts.EnemyAI
{

    public class EnemyBlackboard
    {
        public Transform Target { get; private set; }

        public EnemyDataSO Data {  get; private set; }

        public EnemyBlackboard(Transform target, EnemyDataSO enemyData) {
        
            this.Target = target;
            this.Data = enemyData;
        
        }

    }

}

