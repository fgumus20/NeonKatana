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
            InitializeAnimator();
        }

        private void InitializeAnimator()
        {
            if (_animator != null) return;
            _animator = GetComponentInChildren<Animator>();
        }
        public void PlayRun()
        {
            InitializeAnimator();
            _animator.SetTrigger(RunTrigger);
        }

        public void PlayAttack()
        {
            InitializeAnimator();
            _animator.SetTrigger(AttackTrigger);
        }
    }
}