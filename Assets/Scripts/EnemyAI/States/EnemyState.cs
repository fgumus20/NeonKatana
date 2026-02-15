namespace Scripts.EnemyAI.States
{
    public abstract class EnemyState
    {
        protected readonly EnemyController controller;
        protected EnemyBlackboard enemyBlackboard;
        protected EnemyAnimationManager animationManager;

        public EnemyState(EnemyController controller, EnemyBlackboard enemyBlackboard, EnemyAnimationManager animationManager)
        {
            this.controller = controller;
            this.enemyBlackboard = enemyBlackboard;
            this.animationManager = animationManager;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}
