using System;

public interface ICommandBus 
{
    void AddListener<T>(Action<T> listener) where T : ICommand;
    void RemoveListener<T>(Action<T> listener) where T : ICommand;

    void Dispatch(ICommand command);

    bool Execute(IExecutableCommand command);
}
