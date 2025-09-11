using UnityEngine;

public class GeneratorInstaller : MonoBehaviour, IDamageable
{
    private IContext _context;

    private GeneratorHealthModel _healthModel;
    [SerializeField] private GeneratorHealthView _healthView;
    private GeneratorHealthController _healthController;

    private GeneratorStatsModel _statsModel;
    [SerializeField] private GeneratorStatsView _statsView;
    private GeneratorStatsController _statsController;

    private Generator _generator;
    private void Start()
    {
        _context = new BaseContext();

        _healthModel = new GeneratorHealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new GeneratorHealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        _statsModel = new GeneratorStatsModel();
        _statsModel.Initialise(_context);
        _context.ModelLocator.Register(_statsModel);

        _statsView.Initialise(_context);
        _context.ViewLocator.Register(_statsView);

        _statsController = new GeneratorStatsController(_statsModel, _statsView);
        _statsController.Initialise(_context);


        _generator = GetComponent<Generator>();
        _generator.Initialise(_context);
    }
    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}
