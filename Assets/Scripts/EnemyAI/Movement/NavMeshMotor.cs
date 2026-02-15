using UnityEngine;
using UnityEngine.AI;

namespace Scripts.EnemyAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMotor : MonoBehaviour, IMovementMotor
    {
        private NavMeshAgent agent;

        [Header("Repath Tuning")]
        [SerializeField] private float repathInterval = 0.12f;
        [SerializeField] private float destinationThreshold = 0.30f;

        [Header("Follow Smoothing")]
        [SerializeField] private float followSmoothing = 10f;

        private float _nextRepathTime;
        private Vector3 _lastDestination;
        private Vector3 _smoothedTarget;

        public float RemainingDistance => agent.hasPath ? agent.remainingDistance : Mathf.Infinity;
        public bool HasPath => agent.hasPath;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            _lastDestination = transform.position;
            _smoothedTarget = transform.position;
        }

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }

        public void SetStoppingDistance(float stopDistance)
        {
            agent.stoppingDistance = stopDistance;
        }

        public void MoveTo(Vector3 targetPos)
        {
            _smoothedTarget = Vector3.Lerp(
                _smoothedTarget,
                targetPos,
                1f - Mathf.Exp(-followSmoothing * Time.deltaTime)
            );

            targetPos = _smoothedTarget;

            if (Time.time < _nextRepathTime) return;

            if ((_lastDestination - targetPos).sqrMagnitude < destinationThreshold * destinationThreshold)
                return;

            _nextRepathTime = Time.time + repathInterval;
            _lastDestination = targetPos;

            if (agent.isStopped) agent.isStopped = false;
            agent.SetDestination(targetPos);
        }

        public void Stop()
        {
            agent.isStopped = true;
            agent.ResetPath();

            // stop sonrasý biriken hedefleri sýfýrla
            _nextRepathTime = 0f;
            _lastDestination = transform.position;
            _smoothedTarget = transform.position;
        }
    }
}
