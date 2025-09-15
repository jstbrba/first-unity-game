using UnityEngine;

public class TimeInstaller : MonoBehaviour
{
    public IContext Context { get { return _context; } }

    private IContext _context;

    private TimeModel _model;
    [SerializeField] private TimeView _view;
    private TimeController _controller;

    private void Start()
    {
        _context = new TimeContext();

        _model = new TimeModel();
        _model.Initialise(_context);
        _context.ModelLocator.Register(_model);

        _view.Initialise(_context);
        _context.ViewLocator.Register(_view);

        _controller = new TimeController(_model, _view);
        _controller.Initialise(_context);
    }
    private void Update()
    {
        _controller.Update();
    }
    private void FixedUpdate()
    {
        _controller.Update();
    }
}