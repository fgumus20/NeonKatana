using UnityEngine;

namespace Scripts.EnemyAI.Vfx
{
    public class EnemyVfxController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;

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