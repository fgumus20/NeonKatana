using UnityEngine;

namespace Scripts.EnemyAI
{
    public class EnemyAnimationManager : MonoBehaviour
    {
        private Animator _animator;


        private static readonly int RunTrigger = Animator.StringToHash("run");
        private static readonly int AttackTrigger = Animator.StringToHash("attack");

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void PlayRun()
        {
            _animator.SetTrigger(RunTrigger);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(AttackTrigger);
        }
    }
}