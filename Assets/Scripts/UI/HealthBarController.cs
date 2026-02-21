using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private Image healthFillImage;

    private void OnEnable()
    {
        GameEvents.PlayerHpChanged += HandleHpChanged;
    }

    private void OnDisable()
    {
        GameEvents.PlayerHpChanged -= HandleHpChanged;
    }

    private void HandleHpChanged(int current, int max)
    {
        healthFillImage.fillAmount = (float)current / max;
    }
}