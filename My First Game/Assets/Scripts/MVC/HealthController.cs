public class HealthController 
{
    private readonly HealthModel _model;
    private readonly IContext _context;

    public HealthController(HealthModel model, IContext context)
    {
        _model = model;
        _context = context;
    }
    public void Initialise()
    {
        _model.Health.onValueChanged += OnHealthChanged;
    }
    public void OnHealthChanged(int previous, int current)
    {
        _context.CommandBus.Dispatch(new HealthChangedCommand(previous, current));

        if (current == 0)
            _context.CommandBus.Dispatch(new PlayerDeathCommand());
    }
}