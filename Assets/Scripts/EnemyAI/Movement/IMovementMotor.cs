using UnityEngine;

namespace Scripts.EnemyAI
{
    public interface IMovementMotor
    {
        void SetSpeed(float speed);
        void SetStoppingDistance(float stopDistance);

        void MoveTo(Vector3 targetPos);
        void Stop();

        float RemainingDistance { get; }
        bool HasPath { get; }
    }
}
