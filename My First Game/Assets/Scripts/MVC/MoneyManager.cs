using UnityEngine;
public class MoneyManager : MonoBehaviour {

    private IContext _context;
    private MoneyController _controller;
    private MoneyModel _model;

    public IContext Context { get { return _context; } }
    public MoneyController Controller { get { return _controller; } }
    public MoneyModel Model { get { return _model; } }

    [SerializeField] private MoneyView _view;

    private void Awake()
    {
        _context = new BaseContext();

        _model = new MoneyModel();
        _model.Initialise();
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context, 0);
        _context.ViewLocator.Register(_view);

        _controller = new MoneyController(_model, _context);
        _controller.Initialise();
    }
    public void Controller_AddMoney(int amount)
    {
        _controller.AddMoney(amount);
    }
}
