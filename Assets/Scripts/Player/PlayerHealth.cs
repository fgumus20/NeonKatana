using UnityEngine;

namespace Scripts.Player
{

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int maxHp = 3;
        [SerializeField] private float invulnSeconds = 0.5f;

        public int CurrentHp { get; private set; }

        private float _invulnUntil;

        private void Start()
        {
            CurrentHp = maxHp;
            GameEvents.RaisePlayerHpChanged(CurrentHp, maxHp);
        }

        public void TakeDamage(int amount = 1)
        {
            if (Time.time < _invulnUntil) return;
            _invulnUntil = Time.time + invulnSeconds;

            CurrentHp = Mathf.Max(0, CurrentHp - amount);
            GameEvents.RaisePlayerHpChanged(CurrentHp, maxHp);

            if (CurrentHp == 0)
            {
                GameEvents.RaiseGameOver();
            }
        }
    }
}

