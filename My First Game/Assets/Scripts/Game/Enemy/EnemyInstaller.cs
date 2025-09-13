using Game;
using UnityEngine;

public class EnemyInstaller : MonoBehaviour, IDamageable
{
    private IContext _context;
    // -------------------- META --------------------
    // Enemy Health MVC
    private HealthModel _healthModel;
    [SerializeField] private HealthView _healthView;
    private HealthController _healthController;

    // Enemy Stats MVC
    private EnemyStatsModel _statsModel;
    [SerializeField] private EnemyStatsView _statsView;
    private EnemyStatsController _statsController;

    // -------------------- CORE --------------------
    private Enemy _enemy;
    private void Start()
    {
        _context = new EnemyContext();

        // Initialise Health MVC
        _healthModel = new HealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ModelLocator.Register(_healthView);

        _healthController = new HealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        // Initialise Stats MVC
        _statsModel = new EnemyStatsModel();
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
