using Scripts.Combat;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerAnimationManager : MonoBehaviour
    {
        private Animator _animator;
        private CombatController _combatController;

        private static readonly int IsMoving = Animator.StringToHash("isMoving");

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _combatController = GetComponentInParent<CombatController>();
        }

        private void OnEnable()
        {
            _combatController.OnDashSegmentStarted += HandleDashAnimation;
        }

        private void OnDisable()
        {
            _combatController.OnDashSegmentStarted -= HandleDashAnimation;
        }


        private void HandleDashAnimation(int index)
        {
            _animator.SetInteger("animIndex", index % 2);
            _animator.SetTrigger("attack");
        }

        public void SetMoving(bool moving)
        {
            if (_animator == null) return;
            _animator.SetBool(IsMoving, moving);
        }
    }
}