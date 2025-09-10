using UnityEngine;

public class PlayerHealthController : BaseController<PlayerHealthModel, PlayerHealthView>
{
    public PlayerHealthController(PlayerHealthModel model, PlayerHealthView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.CurrentHealth.onValueChanged += Model_CurrentHealth_OnValueChanged;
        _model.MaxHealth.onValueChanged += Model_MaxHealth_OnValueChanged;

        Context.CommandBus.AddListener<ApplyDamageCommand>(ApplyDamage);

        Context.CommandBus.Dispatch(new MaxHealthChangedCommand(_model.CurrentHealth.Value, _model.CurrentHealth.Value));
    }
    public void ApplyDamage(ApplyDamageCommand command)
    {
        _model.CurrentHealth.Value = Mathf.Max(0, _model.CurrentHealth.Value - command.Damage);
    }
    public void Model_CurrentHealth_OnValueChanged(int previous, int current)
    {
        Context.CommandBus.Dispatch(new HealthChangedCommand(previous, current));

        if (_model.CurrentHealth.Value == 0)
            Context.CommandBus.Dispatch(new DeathCommand());
    }
    public void Model_MaxHealth_OnValueChanged(int previous, int current)
    {
        Context.CommandBus.Dispatch(new MaxHealthChangedCommand(previous, current));
    }
}
