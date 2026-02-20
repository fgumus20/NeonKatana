using UnityEngine;
using System.Collections.Generic;
using Scripts.Combat.Vfx;

namespace Scripts.Combat
{
    public class CombatBlackboard
    {
        public Transform PlayerTransform { get; private set; }
        public Rigidbody PlayerRigidbody { get; private set; }
        public PlayerStatsSO PlayerStats { get; private set; }

        public List<DashCommand> DashPoints = new List<DashCommand>();

        public CombatBlackboard(Transform tr, Rigidbody rb, PlayerStatsSO stats)
        {
            PlayerTransform = tr;
            PlayerRigidbody = rb;
            PlayerStats = stats;
        }
        public void ClearPoints() => DashPoints.Clear();
    }
}
