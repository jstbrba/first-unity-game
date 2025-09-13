using System.Collections.Generic;
using System;

public static class ContextLocator
{
    private static readonly Dictionary<Type, List<IContext>> _contexts = new();

    public static void Register(IContext context)
    {
        var type = context.GetType();

        if (!_contexts.ContainsKey(type)) _contexts[type] = new List<IContext>();
        _contexts[type].Add(context);
    }
    public static void Unregister(IContext context)
    {
        var type = context.GetType();

        if (_contexts.TryGetValue(type, out var list))
        {
            list.Remove(context);
            if (list.Count == 0)
                _contexts.Remove(type);
        }
    }
    public static IEnumerable<T> Get<T>() where T: IContext
    {
        var type = typeof (T);

        if ( _contexts.TryGetValue(type,out var list))
            foreach (var cxt in list)
                yield return (T)cxt;
    }
}
