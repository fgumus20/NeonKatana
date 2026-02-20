using UnityEngine;
using UnityEngine.AI;

namespace Scripts.EnemyAI
{

    public class EnemyBlackboard
    {
        public Transform Target { get; private set; }

        public EnemyDataSO Data {  get; private set; }

        public NavMeshAgent Agent { get; private set; }

        public EnemyBlackboard(Transform target, EnemyDataSO enemyData, NavMeshAgent agent)
        {

            this.Target = target;
            this.Data = enemyData;
            this.Agent = agent;
        }

        public void SetTarget(Transform target) => Target = target;

    }

}

