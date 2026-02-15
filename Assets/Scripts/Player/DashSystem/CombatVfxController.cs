using UnityEngine;

namespace Scripts.Combat.Vfx
{
    public class CombatVfxController : MonoBehaviour
    {
        [Header("Sword Trail")]
        [SerializeField] private TrailRenderer swordTrail;   // kýlýcýn child’ýndaki trail

        private void Awake()
        {
            swordTrail.emitting = false;
        }

        public void PlayDashSegmentEffects()
        {

            EnableTrail();
        }

        public void StopDashSegmentEffects()
        {
            DisableTrail();
        }

        private void EnableTrail()
        {
            swordTrail.emitting = true;
        }

        private void DisableTrail()
        {
            swordTrail.emitting = false;
        }

    }
}
