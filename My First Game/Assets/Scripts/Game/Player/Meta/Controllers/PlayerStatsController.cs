using UnityEngine;

public class PlayerStatsController : BaseController<PlayerStatsModel, PlayerStatsView>
{
    public PlayerStatsController(PlayerStatsModel model, PlayerStatsView view) : base(model, view) { }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        Context.CommandBus.AddListener<UpgradeSpeedCommand>(UpgradeSpeed);
        Context.CommandBus.AddListener<UpgradeAttackCommand>(UpgradeAttack);
    }
    public void UpgradeSpeed(UpgradeSpeedCommand command)
    {
        _model.Speed.Value += command.Speed;
    }

    public void UpgradeAttack(UpgradeAttackCommand command) => _model.Attack.Value += command.Attack;
}
