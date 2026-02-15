namespace Scripts.EnemyAI
{
    public interface IAttackBehaviour
    {
        void ExecuteAttack(EnemyController controller, EnemyBlackboard blackboard, EnemyAnimationManager animManager);
    }

}