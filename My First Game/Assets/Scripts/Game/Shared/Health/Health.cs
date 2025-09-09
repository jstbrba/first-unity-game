using UnityEngine;
public class Health
{
    public IContext _context {  get; private set; }
    public HealthModel Model { get { return _model; } }
    public HealthController Controller { get { return _controller; } }

    private readonly HealthView _view;
    private HealthController _controller;
    private HealthModel _model;
    private int _maxHealth = 10;

    public Health(IContext context, HealthView view, int maxHealth)
    {
        _context = context;
        _view = view;
        _maxHealth = maxHealth;
    }

    public void Initialise()
    {
        _context = new BaseContext();

        _model = new HealthModel();
        _model.Initialise(_maxHealth);
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context, _model.MaxHealth.Value);
        _context.ViewLocator.Register(_view);

        _controller = new HealthController(_model, _context);
        _controller.Initialise();
    }
}
