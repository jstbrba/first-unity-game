using UnityEngine;

public abstract class BaseController <TModel, TView> : IController
{
    public IContext Context { get { return _context; } }
    private IContext _context;
    protected readonly TModel _model;
    protected readonly TView _view;

    public BaseController(TModel model, TView view)
    {
        _model = model;
        _view = view;
    }
    public virtual void Initialise(IContext context)
    {
        _context = context;
    }
}
