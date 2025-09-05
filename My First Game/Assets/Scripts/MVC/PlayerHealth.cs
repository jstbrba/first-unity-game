using UnityEngine;
public class PlayerHealth : MonoBehaviour 
{
    private IContext _context;
    private HealthController _controller;
    private HealthModel _model;

    public IContext Context {  get { return _context; } }
    public HealthController Controller { get { return _controller; } }
    public HealthModel Model { get { return _model; } }

    [SerializeField] private HealthView _view;
    [SerializeField] private int maxHealth = 20;

    private void Awake()
    {
        _context = new BaseContext();

        _model = new HealthModel();
        _model.Initialise(maxHealth);
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context, _model.MaxHealth);
        _context.ViewLocator.Register(_view);

        _controller = new HealthController(_model, _context);
        _controller.Initialise();
    }
}
