using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI.States
{

    public abstract class EnemyState
    {
        protected EnemyController controller;

        public EnemyState(EnemyController controller)
        {
            this.controller = controller;
        }

        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }

}
