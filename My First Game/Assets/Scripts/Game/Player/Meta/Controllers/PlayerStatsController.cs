using UnityEngine;

public class PlayerStatsController : BaseController<PlayerStatsModel, PlayerStatsView>
{
    public PlayerStatsController(PlayerStatsModel model, PlayerStatsView view) : base(model, view) { }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;

        Context.CommandBus.AddListener<SetSpeedCommand>(OnSetSpeed);
        Context.CommandBus.AddListener<SetAttackCommand>(OnSetAttack);
    }
    public void Model_Speed_OnValueChanged(float previous, float current)
    {
        Context.CommandBus.Dispatch(new SpeedChangedCommand(previous, current));
    }
    public void Model_Attack_OnValueChanged(int previous, int current)
    {
        Context.CommandBus.Dispatch(new AttackChangedCommand(previous, current));
    }
    public void OnSetSpeed(SetSpeedCommand command) => _model.Speed.Value = command.Speed;
    public void OnSetAttack(SetAttackCommand command) => _model.Attack.Value = command.Attack;
}
