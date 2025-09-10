using UnityEngine;

public class ShutterHealthView : MonoBehaviour, IView 
{
    public IContext Context { get { return _context; } }
    private IContext _context;

    [SerializeField] private Transform _shutterDoor;
    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<HealthChangedCommand>(OnHealthChanged);
        _context.CommandBus.AddListener<MaxHealthChangedCommand>(OnMaxHealthChanged);
        _context.CommandBus.AddListener<DeathCommand>(OnDeath);
    }
    public void OnHealthChanged(HealthChangedCommand command)
    {
        // change health bar if i ever add one for them
    }
    public void OnMaxHealthChanged(MaxHealthChangedCommand command)
    {
        // change max health for health bar... if i do add one
    }
    public void OnDeath(DeathCommand command)
    {
        _shutterDoor.gameObject.SetActive(false);
    }
}