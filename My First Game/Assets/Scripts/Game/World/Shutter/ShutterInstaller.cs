using UnityEngine;

public class ShutterInstaller : MonoBehaviour, IDamageable 
{
    private IContext _context;

    [SerializeField] private ShutterHealthModel _baseHealthModel;
    private ShutterHealthModel _healthModel;
    [SerializeField] private ShutterHealthView _healthView;
    private ShutterHealthController _healthController;


    private void Start()
    {
        _context = new BaseContext();

        _healthModel = Instantiate(_baseHealthModel);
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new ShutterHealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);
    }
    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}
