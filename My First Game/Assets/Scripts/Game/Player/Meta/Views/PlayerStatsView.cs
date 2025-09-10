using UnityEngine;

public class PlayerStatsView : MonoBehaviour, IView
{
    // View will be later
    public IContext Context { get { return _context; } }

    private IContext _context;
    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<SpeedChangedCommand>(OnSpeedChanged);
        _context.CommandBus.AddListener<AttackChangedCommand>(OnAttackChanged);
    }
    public void OnSpeedChanged(SpeedChangedCommand command)
    {
        // noop
    }
    public void OnAttackChanged(AttackChangedCommand command) 
    {
        // noop
    }
}
