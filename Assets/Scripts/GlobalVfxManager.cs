using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Scripts.Managers
{
    public class GlobalVfxManager : MonoBehaviour
    {
        public static GlobalVfxManager Instance { get; private set; }

        [Header("Death Particles")]
        [SerializeField] private GameObject enemyDeathVfx;

        [Header("Camera Shake Settings")]
        [SerializeField] private float shakeIntensity = 0.3f;
        [SerializeField] private float shakeDuration = 0.15f;
        [SerializeField] private float shakeCooldown = 0.1f;

        private Camera _mainCamera;
        private float _lastShakeTime;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            GameEvents.EnemyDied += HandleEnemyDeath;
        }

        private void OnDisable()
        {
            GameEvents.EnemyDied -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath(GameObject enemy)
        {
            SpawnDeathVfx(enemy.transform.position);

            TriggerImpactEffects();
        }

        private void SpawnDeathVfx(Vector3 position)
        {
            if (enemyDeathVfx == null) return;

            var vfx = Instantiate(enemyDeathVfx, position + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(vfx.gameObject, 2f);
        }

        private void TriggerImpactEffects()
        {
            if (Time.time < _lastShakeTime + shakeCooldown) return;

            _mainCamera.transform.DOComplete();
            _mainCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, 14, 90, false, true);

            _lastShakeTime = Time.time;
        }


    }
}