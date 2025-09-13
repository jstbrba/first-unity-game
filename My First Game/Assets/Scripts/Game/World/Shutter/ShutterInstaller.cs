using UnityEngine;

public class ShutterInstaller : MonoBehaviour, IDamageable 
{
    private IContext _context;

    private HealthModel _healthModel;
    [SerializeField] private HealthView _healthView;
    private HealthController _healthController;

    private Shutter _shutter;
    private void Start()
    {
        _context = new ShutterContext();

        _healthModel = new HealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new HealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        _shutter = GetComponent<Shutter>();
        _shutter.Intitialise(_context);
    }
    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}
