using UnityEngine;
public class MoneyInstaller : MonoBehaviour {

    private IContext _context;

    private MoneyModel _model;
    [SerializeField] private MoneyView _view;
    private MoneyController _controller;

    private void Awake()
    {
        _context = new BaseContext();

        _model = new MoneyModel();
        _model.Initialise();
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context);
        _context.ViewLocator.Register(_view);

        _controller = new MoneyController(_model, _view);
        _controller.Initialise(_context);
    }
    public void Controller_AddMoney(int amount)
    {
        _controller.AddMoney(amount);
    }
}
