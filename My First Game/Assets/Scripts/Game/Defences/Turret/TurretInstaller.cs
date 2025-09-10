using UnityEngine;

public class TurretInstaller : MonoBehaviour, IDamageable
{
    private IContext _context;

    // -------------------- META --------------------
    private TurretHealthModel _healthModel;
    [SerializeField] private TurretHealthView _healthView;
    private TurretHealthController _healthController;

    // -------------------- CORE --------------------
    private Turret _turret;


    private void Start()
    {

        _context = new BaseContext();
        // -------------------- META --------------------
        // Initialise Health MVC
        _healthModel = new TurretHealthModel();
        _healthModel.Initialise(_context);
        _context.ModelLocator.Register(_healthModel);

        _healthView.Initialise(_context);
        _context.ViewLocator.Register(_healthView);

        _healthController = new TurretHealthController(_healthModel, _healthView);
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