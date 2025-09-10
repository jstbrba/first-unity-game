using UnityEngine;

public class EnemyStatsView : MonoBehaviour, IView
{
    public IContext Context { get { return _context; } }

    private IContext _context;
    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<SpeedChangedCommand>(OnSpeedChanged);
        _context.CommandBus.AddListener<AttackChangedCommand>(OnAttackChanged);
        _context.CommandBus.AddListener<DeathMoneyChangedCommand>(OnDeathMoneyChanged);
    }
    public void OnSpeedChanged(SpeedChangedCommand command)
    {
        // noop
    }
    public void OnAttackChanged(AttackChangedCommand command)
    {
        // noop
    }
    public void OnDeathMoneyChanged(DeathMoneyChangedCommand command) 
    {
        // noop
    }
}
