using System;
using System.Collections.Generic;

public class CommandBus : ICommandBus
{
    private readonly IContext _context;
    private readonly Dictionary<Type, List<Action<ICommand>>> _listeners = new();

    public CommandBus(IContext context)
    {
        _context = context;
    }

    public void AddListener<T>(Action<T> listener) where T : ICommand
    {
        var type = typeof(T);
        if (!_listeners.ContainsKey(type))
            _listeners[type] = new List<Action<ICommand>>();

        _listeners[type].Add(command => listener((T)command));
    }

    public void RemoveListener<T>(Action<T> listener) where T : ICommand
    {
        var type = typeof(T);
        if (_listeners.TryGetValue(type, out var list))
        {
            list.RemoveAll(a => a.Equals(listener));
        }
    }

    public void Dispatch(ICommand command)
    {
        var type = command.GetType();
        if (_listeners.TryGetValue(type, out var listeners))
        {
            foreach (var listener in listeners)
                listener(command);
        }
    }

    public bool Execute(IExecutableCommand command)
    {
        bool result = command.Execute(_context);
        Dispatch(command);
        return result;
    }
}
