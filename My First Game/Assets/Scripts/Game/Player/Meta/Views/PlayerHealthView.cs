using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour, IView
{
    [SerializeField] private Image healthBar;
    public IContext Context { get { return _context; } }
    private IContext _context;

    private int _maxHealth;
    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<HealthChangedCommand>(OnHealthChanged);
        _context.CommandBus.AddListener<MaxHealthChangedCommand>(OnMaxHealthChanged);
        _context.CommandBus.AddListener<DeathCommand>(OnDeath);

    }
    private void OnHealthChanged(HealthChangedCommand command)
    {
        healthBar.fillAmount = (float)command.Current / _maxHealth;
    }
    private void OnMaxHealthChanged(MaxHealthChangedCommand command)
    {
        _maxHealth = command.Current;
    }
    private void OnDeath(DeathCommand command) 
    {
        gameObject.SetActive(false);
    }
}
