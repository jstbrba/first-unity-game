public class BaseContext : IContext
{
    public ICommandBus CommandBus { get { return _commandBus; } }

    public ModelLocator ModelLocator { get { return _modelLocator; } }

    public ViewLocator ViewLocator { get { return _viewLocator; } }
    private readonly ICommandBus _commandBus;
    private readonly ModelLocator _modelLocator;
    private readonly ViewLocator _viewLocator;

    public BaseContext()
    {
        _modelLocator = new ModelLocator();
        _viewLocator = new ViewLocator();
        _commandBus = new CommandBus(this);
    }
}