namespace Scripts.Combat
{
    public abstract class CombatState
    {
        protected readonly CombatBlackboard CombatBlackboard;
        protected CombatController CombatController;
        protected CombatState(CombatController controller, CombatBlackboard blackboard)
        {
            CombatBlackboard = blackboard;
            CombatController = controller;
        }

        public abstract void OnEnter();
        public abstract void Update();
        public abstract void OnExit();
    }

}

