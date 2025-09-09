using System;
using System.Collections.Generic;

public class ModelLocator 
{
    private readonly Dictionary<Type, object> _models = new();

    public void Register<T>(T model) => _models[typeof(T)] = model;
    public T Get<T>() => (T) _models[typeof(T)];
}
