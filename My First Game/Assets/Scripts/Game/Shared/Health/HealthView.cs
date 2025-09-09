using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class HealthView : MonoBehaviour
{
    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private Image healthBar;
    private IContext _context;
    private int _maxHealth;

    public void Initialise(IContext context, int maxHealth)
    {
        _context = context;
        _maxHealth = maxHealth;
        if (healthBar != null) healthBar.fillAmount = 1f;

        _context.CommandBus.AddListener<HealthChangedCommand>(OnHealthChanged);
        _context.CommandBus.AddListener<MaxHealthChangedCommand>(OnMaxHealthChanged);
        _context.CommandBus.AddListener<DeathCommand>(OnPlayerDeath);
    }
    public void OnHealthChanged(HealthChangedCommand command)
    {
        if (healthBar != null) healthBar.fillAmount = (float)command.Current / _maxHealth;
    }
    public void OnMaxHealthChanged(MaxHealthChangedCommand command)
    {
        _maxHealth = command.Current;
    }
    public void OnPlayerDeath(DeathCommand command) => OnDeath?.Invoke();
}
