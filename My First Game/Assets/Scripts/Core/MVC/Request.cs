public abstract class Request : ICommand 
{
    public IContext ReturnContext { get; }

    public Request(IContext returnContext)
    {
        ReturnContext = returnContext;
    }
}
