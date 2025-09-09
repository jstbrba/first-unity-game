using System;
using System.Collections.Generic;

public class ViewLocator 
{
    private readonly Dictionary<Type, object> _views = new();

    public void Register<T>(T view) => _views[typeof(T)] = view;
    public T Get<T>() => (T) _views[typeof(T)];
}
