using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    private Animator _animator;


    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");

    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void SetMoving(bool moving)
    {
        if (_animator == null) return;
        _animator.SetBool(IsMoving, moving);
    }

    public void PlayAttack()
    {
        if (_animator == null) return;
        _animator.SetTrigger(AttackTrigger);
    }
}