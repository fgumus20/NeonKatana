using UnityEngine;
using System.Collections.Generic;
using Scripts.Combat.Vfx;

namespace Scripts.Combat
{
    public class CombatBlackboard
    {
        public Transform PlayerTransform { get; private set; }
        public Rigidbody PlayerRigidbody { get; private set; }
        public Animator PlayerAnimator { get; private set; }
        public PlayerStatsSO PlayerStats { get; private set; }
        public CombatVfxController CombatVfxController { get; private set; }

        public List<DashCommand> DashPoints = new List<DashCommand>();

        public CombatBlackboard(Transform tr, Rigidbody rb, Animator anim, PlayerStatsSO stats, CombatVfxController vfxController)
        {
            PlayerTransform = tr;
            PlayerRigidbody = rb;
            PlayerAnimator = anim;
            PlayerStats = stats;
            CombatVfxController = vfxController;
            
        }
        public void ClearPoints() => DashPoints.Clear();
    }
}
