using Game;
using UnityEngine;

public class PlayerInstaller : MonoBehaviour, IDamageable
{

    private IContext _context;
    // -------------------- META --------------------
    // Health MVC
    private HealthModel _healthModel;
    [SerializeField] private PlayerHealthView _healthView;
    private HealthController _healthController;

    // Stats MVC
    private PlayerStatsModel _statsModel;
    [SerializeField] private PlayerStatsView _statsView;
    private PlayerStatsController _statsController;

    // -------------------- CORE --------------------
    private PlayerMovement _movement;
    private PlayerStateMachine _stateMachine;
    private void Start()
    {
        _context = new PlayerContext();
        // -------------------- META --------------------
        // Initialise Health MVC
         _healthModel = new HealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new HealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        // Initialise Stats MVC
         _statsModel = new PlayerStatsModel();
        _statsModel.Initialise(_context);
        _context.ModelLocator.Register(_statsModel);

        _statsView.Initialise(_context);
        _context.ViewLocator.Register(_statsView);

        _statsController = new PlayerStatsController(_statsModel, _statsView);
        _statsController.Initialise(_context);

        // -------------------- CORE --------------------
        _movement = GetComponent<PlayerMovement>();
        _stateMachine = GetComponent<PlayerStateMachine>();

        _movement.Initialise(_context);
        _stateMachine.Initialise(_context);
    }

    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}
