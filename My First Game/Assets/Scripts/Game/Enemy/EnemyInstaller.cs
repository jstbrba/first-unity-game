using Game;
using UnityEngine;

public class EnemyInstaller : MonoBehaviour, IDamageable
{
    private IContext _context;
    // -------------------- META --------------------
    // Enemy Health MVC
    [SerializeField] private EnemyHealthModel _baseHealthModel;
    private EnemyHealthModel _healthModel;
    [SerializeField] private EnemyHealthView _healthView;
    private EnemyHealthController _healthController;

    // Enemy Stats MVC
    [SerializeField] private EnemyStatsModel _baseStatsModel;
    private EnemyStatsModel _statsModel;
    [SerializeField] private EnemyStatsView _statsView;
    private EnemyStatsController _statsController;

    // -------------------- CORE --------------------
    private Enemy _enemy;
    private void Start()
    {
        _context = new BaseContext();

        // Initialise Health MVC
        _healthModel = Instantiate(_baseHealthModel);
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ModelLocator.Register(_healthView);

        _healthController = new EnemyHealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        // Initialise Stats MVC
        _statsModel = Instantiate(_baseStatsModel);
        _statsModel.Initialise(_context);
        _context.ModelLocator.Register(_statsModel);

        _statsView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _statsController = new EnemyStatsController(_statsModel, _statsView);
        _statsController.Initialise(_context);

        // -------------------- CORE --------------------
        _enemy = GetComponent<Enemy>();
        _enemy.Initialise(_context);
    }

    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}
