namespace Scripts.EnemyAI.States
{
    public abstract class EnemyState
    {
        protected readonly EnemyController controller;
        protected EnemyBlackboard enemyBlackboard;

        public EnemyState(EnemyController controller, EnemyBlackboard enemyBlackboard)
        {
            this.controller = controller;
            this.enemyBlackboard = enemyBlackboard;
        }

        public abstract void OnEnter();
        public abstract void OnUpdate();
        public abstract void OnExit();
    }
}
