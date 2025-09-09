public interface IExecutableCommand : ICommand
{
    bool Execute(IContext context);
    bool Undo(IContext context);
    bool CanUndo { get; }
}