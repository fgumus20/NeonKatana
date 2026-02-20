using DG.Tweening;
using UnityEngine;

namespace Scripts.EnemyAI.Vfx
{
    public class EnemyVfxController : MonoBehaviour
    {
        public GameObject deathParticles;
        [Header("Shake Settings")]
        [SerializeField] private float shakeIntensity = 0.2f;
        [SerializeField] private float shakeDuration = 0.15f;
        [SerializeField] private float shakeCooldown = 0.2f;

        private float _lastShakeTime;
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            GameEvents.EnemyDied += HandleGlobalEnemyDeath;
        }

        private void OnDisable()
        {
            GameEvents.EnemyDied -= HandleGlobalEnemyDeath;
        }

        private void HandleGlobalEnemyDeath(GameObject enemy)
        {

            PlayDeathEffect();
            TriggerImpactEffects();
            
        }

        private void TriggerImpactEffects()
        {
            if (Time.time < _lastShakeTime + shakeCooldown) return;

            _mainCamera.transform.DOComplete();
            _mainCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, 14, 90);

            _lastShakeTime = Time.time;

        }
        public void PlayDeathEffect()
        {
            if (deathParticles != null)
            {
                var vfx = Instantiate(deathParticles, transform.position + Vector3.up, Quaternion.identity);
                Destroy(vfx.gameObject, 2f);
            }
        }
    }
}