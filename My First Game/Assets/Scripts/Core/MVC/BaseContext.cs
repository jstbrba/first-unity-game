public class BaseContext : IContext
{
    public ICommandBus CommandBus { get { return _commandBus; } }

    public ModelLocator ModelLocator { get { return _modelLocator; } }

    public ViewLocator ViewLocator { get { return _viewLocator; } }
    protected readonly ICommandBus _commandBus;
    protected readonly ModelLocator _modelLocator;
    protected readonly ViewLocator _viewLocator;

    public BaseContext()
    {
        _modelLocator = new ModelLocator();
        _viewLocator = new ViewLocator();
        _commandBus = new CommandBus(this);

        ContextLocator.Register(this);
    }
}
