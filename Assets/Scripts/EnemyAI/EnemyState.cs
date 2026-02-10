using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.EnemyAI
{

    public abstract class EnemyState
    {
        protected readonly EnemyController c;
        protected EnemyState(EnemyController controller)
        {

            this.c = controller;

        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }
    }


}
