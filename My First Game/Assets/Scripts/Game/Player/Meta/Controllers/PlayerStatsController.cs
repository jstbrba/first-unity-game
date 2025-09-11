using UnityEngine;

public class PlayerStatsController : BaseController<PlayerStatsModel, PlayerStatsView>
{
    public PlayerStatsController(PlayerStatsModel model, PlayerStatsView view) : base(model, view) { }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;
        _model.JumpPower.onValueChanged += Model_JumpPower_OnValueChanged;
    }
    public void Model_Speed_OnValueChanged(float previous, float current)
    {
        Context.CommandBus.Dispatch(new SpeedChangedCommand(previous, current));
    }
    public void Model_Attack_OnValueChanged(int previous, int current)
    {
        Context.CommandBus.Dispatch(new AttackChangedCommand(previous, current));
    }
    public void Model_JumpPower_OnValueChanged(float previous, float current)
    {
        Context.CommandBus.Dispatch(new JumpPowerChangedCommand(previous, current));
    }
}
