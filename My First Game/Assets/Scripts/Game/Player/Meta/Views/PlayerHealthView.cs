using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : HealthView
{
    [SerializeField] private Image healthBar;

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
    }
    protected override void Model_CurrentHealth_OnValueChanged(int previous, int current)
    {
        base.Model_CurrentHealth_OnValueChanged(previous, current);
        healthBar.fillAmount = (float)_currentHealth / _maxHealth;

        if (current == 0) gameObject.SetActive(false);
    }
}
