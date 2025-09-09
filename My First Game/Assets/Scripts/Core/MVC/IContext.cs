public interface IContext 
{
    ICommandBus CommandBus { get; }
    ModelLocator ModelLocator { get; }
    ViewLocator ViewLocator { get; }
}
