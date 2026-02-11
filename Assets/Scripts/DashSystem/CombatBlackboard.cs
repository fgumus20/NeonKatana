using UnityEngine;

namespace Scripts.Combat
{
    using UnityEngine;
    using System.Collections.Generic;

    public class CombatBlackboard
    {
        public Transform PlayerTransform { get; private set; }
        public Rigidbody PlayerRigidbody { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public PlayerStatsSO PlayerStats { get; private set; }

        public List<DashCommand> DashPoints = new List<DashCommand>();

        public CombatBlackboard(Transform tr, Rigidbody rb, Animator anim, PlayerStatsSO stats)
        {
            PlayerTransform = tr;
            PlayerRigidbody = rb;
            PlayerAnimator = anim;
            PlayerStats = stats;
            
        }
        public void ClearPoints() => DashPoints.Clear();
    }
}
