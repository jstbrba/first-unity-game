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
        Context.CommandBus.AddListener<RespawnCommand>(OnRespawn);
    }
    private void ApplyDamage(ApplyDamageCommand command)
    {
        _model.CurrentHealth.Value = Mathf.Max(0, _model.CurrentHealth.Value - command.Damage);
    }
    private void UpgradeMaxHealth(UpgradeMaxHealthCommand command)
    {
        _model.MaxHealth.Value += command.Health;
        _model.CurrentHealth.Value = _model.MaxHealth.Value;
        Debug.Log("Health upgraded to " + _model.MaxHealth.Value);
    }

    private void Model_CurrentHealth_OnValueChanged(int previous, int current)
    {
        if (_model.CurrentHealth.Value == 0)
            Context.CommandBus.Dispatch(new DeathCommand());
    }
    private void OnRespawn(RespawnCommand command) => _model.CurrentHealth.Value = _model.MaxHealth.Value;
}
