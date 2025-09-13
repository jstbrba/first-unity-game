using UnityEngine;

public class TurretInstaller : MonoBehaviour, IDamageable
{
    private IContext _context;

    // -------------------- META --------------------
    private HealthModel _healthModel;
    [SerializeField] private HealthView _healthView;
    private HealthController _healthController;

    // -------------------- CORE --------------------
    private Turret _turret;


    private void Start()
    {

        _context = new TurretContext();
        // -------------------- META --------------------
        // Initialise Health MVC
        _healthModel = new HealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new HealthController(_healthModel, _healthView);
        _healthController.Initialise(_context);

        // -------------------- CORE --------------------
        _turret = GetComponent<Turret>();
        _turret.Initialise(_context);
    }
    public void ApplyDamage(int damage)
    {
        _context.CommandBus.Dispatch(new ApplyDamageCommand(damage));
    }
}