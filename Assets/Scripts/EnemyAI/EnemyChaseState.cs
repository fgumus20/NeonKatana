using UnityEngine;


namespace Scripts.EnemyAI.States
{

    public class EnemyChaseState : EnemyState
    {

        private float _repathTimer;
        private const float RepathInterval = .5f;

        public EnemyChaseState(EnemyController controller) : base(controller) { }

        public override void OnEnter()
        {
            controller.agent.isStopped = false;
            //Debug.Log("Chase State'e girildi.");
        }

        public override void OnUpdate()
        {
            if (controller.playerTransform == null) return;

            _repathTimer -= Time.deltaTime;
            if (_repathTimer <= 0f)
            {
                UpdatePath();
                _repathTimer = RepathInterval;
            }

            float distance = Vector3.Distance(controller.transform.position, controller.playerTransform.position);

            if (distance <= controller.data.attackRange)
            {
                //Debug.Log("Saldýrý menziline girildi.");
                controller.ChangeState(new EnemyAnticipationState(controller));
            }
        }

        private void UpdatePath()
        {
            if (controller.agent.isOnNavMesh)
            {
                controller.agent.SetDestination(controller.playerTransform.position);
            }
        }

        public override void OnExit()
        {
            if (controller.agent.isOnNavMesh)
                controller.agent.isStopped = true;
        }
    }



}
