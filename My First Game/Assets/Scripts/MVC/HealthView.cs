using UnityEngine;
using UnityEngine.UI;
public class HealthView : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image healthBar;
    private IContext _context;
    private int _maxHealth;

    public void Initialise(IContext context, int maxHealth)
    {
        _context = context;
        _maxHealth = maxHealth;
        healthBar.fillAmount = 1f;

        _context.CommandBus.AddListener<HealthChangedCommand>(OnHealthChanged);
        _context.CommandBus.AddListener<PlayerDeathCommand>(OnPlayerDeath);
    }
    public void OnHealthChanged(HealthChangedCommand command)
    {
        healthBar.fillAmount = (float)command.Current / _maxHealth;
    }
    public void OnPlayerDeath(PlayerDeathCommand command) => player.SetActive(false);
}
