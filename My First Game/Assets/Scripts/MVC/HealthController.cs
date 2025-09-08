using System;
using UnityEngine;

public class HealthController 
{
    private readonly HealthModel _model;
    private readonly IContext _context;

    public HealthController(HealthModel model, IContext context)
    {
        _model = model;
        _context = context;
    }
    public void Initialise()
    {
        _model.Health.onValueChanged += OnHealthChanged;
        _model.MaxHealth.onValueChanged += OnMaxHealthChanged;
    }


    public void ApplyDamage(int damage)
    {
        _model.Health.Value = Mathf.Max(0, _model.Health.Value - damage);
    }
    public void SetMaxHealth(int newValue)
    {
        _model.MaxHealth.Value = newValue;
    }
    public void OnHealthChanged(int previous, int current)
    {
        _context.CommandBus.Dispatch(new HealthChangedCommand(previous, current));

        if (current == 0)
            _context.CommandBus.Dispatch(new DeathCommand());
    }
    private void OnMaxHealthChanged(int previous, int current)
    {
        _context.CommandBus.Dispatch(new MaxHealthChangedCommand(previous, current));
    }
}