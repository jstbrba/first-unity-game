using UnityEngine;
public class UpgradeMenuInstaller : MonoBehaviour
{
    private IContext _context;

    private UpgradeMenuModel _menuModel;
    [SerializeField] private UpgradeMenuView _menuView;
    private UpgradeMenuController _menuController;

    private void Start()
    {
        _context = new BaseContext();

        _menuModel = new UpgradeMenuModel();
        _menuModel.Initialise(_context);
        _context.ModelLocator.Register(_menuModel);

        _menuView.Initialise(_context);
        _context.ViewLocator.Register(_menuView);

        _menuController = new UpgradeMenuController(_menuModel, _menuView);
        _menuController.Initialise(_context);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) _context.CommandBus.Dispatch(new ToggleMenuCommand());
    }
}