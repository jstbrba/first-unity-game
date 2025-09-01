using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour 
{
    [SerializeField] private Health health;
    [SerializeField] private Image healthBar;

    private void OnEnable()
    {
        health.OnHealthChange += UpdateHealthBar;
    }
    private void OnDisable()
    {
        health.OnHealthChange -= UpdateHealthBar;
    }

    private void UpdateHealthBar(int current, int max)
    {
        healthBar.fillAmount = (float)current / max;
    }
}
