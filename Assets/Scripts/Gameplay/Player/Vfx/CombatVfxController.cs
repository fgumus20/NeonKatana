using UnityEngine;

namespace Scripts.Combat.Vfx
{
    public class CombatVfxController : MonoBehaviour
    {
        [Header("Sword Trail")]
        [SerializeField] private TrailRenderer swordTrail;
        private CombatController _combatController;
        private void Awake()
        {
            swordTrail.emitting = false;
            _combatController = GetComponentInParent<CombatController>();
        }

        private void OnEnable()
        {
            _combatController.OnDashSegmentStarted += HandleVfx;
            _combatController.OnDashSequenceCompleted += StopVfx;
        }

        private void OnDisable()
        {
            _combatController.OnDashSegmentStarted -= HandleVfx;
            _combatController.OnDashSequenceCompleted -= StopVfx;
        }

        private void HandleVfx(int index)
        {
           EnableSwordTrail();
        }

        private void StopVfx()
        {
            swordTrail.emitting = false;
        }

        private void EnableSwordTrail()
        {
            swordTrail.emitting = true;
        }

    }
}
