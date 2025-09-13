using UnityEngine;

public class HealthController : BaseController<HealthModel, HealthView>
{
    public HealthController(HealthModel model, HealthView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.CurrentHealth.onValueChanged += Model_CurrentHealth_OnValueChanged;

        Context.CommandBus.AddListener<UpgradeMaxHealthCommand>(UpgradeMaxHealth);
        Context.CommandBus.AddListener<ApplyDamageCommand>(ApplyDamage);
    }
    public void ApplyDamage(ApplyDamageCommand command)
    {
        _model.CurrentHealth.Value = Mathf.Max(0, _model.CurrentHealth.Value - command.Damage);
    }
    public void UpgradeMaxHealth(UpgradeMaxHealthCommand command) => _model.MaxHealth.Value += command.Health;
    public void Model_CurrentHealth_OnValueChanged(int previous, int current)
    {
        if (_model.CurrentHealth.Value == 0)
            Context.CommandBus.Dispatch(new DeathCommand());
    }
}
