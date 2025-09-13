using UnityEngine;

public class EnemyStatsController : BaseController<EnemyStatsModel, EnemyStatsView>
{
    public EnemyStatsController(EnemyStatsModel model, EnemyStatsView view) : base(model, view) { }

    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        Context.CommandBus.AddListener<UpgradeSpeedCommand>(UpgradeSpeed);
        Context.CommandBus.AddListener<UpgradeAttackCommand>(UpgradeAttack);
        Context.CommandBus.AddListener<IncreaseMoneyOnDeathCommand>(IncreaseMoneyOnDeath);

        Context.CommandBus.AddListener<DeathCommand>(Health_OnDeath);
    }
    public void UpgradeSpeed(UpgradeSpeedCommand command) => _model.Speed.Value += command.Speed;
    public void UpgradeAttack(UpgradeAttackCommand command) => _model.Attack.Value += command.Attack;
    public void IncreaseMoneyOnDeath(IncreaseMoneyOnDeathCommand command) => _model.MoneyOnDeath.Value += command.MoneyOnDeath;
    public void Health_OnDeath(DeathCommand command)
    {
        // send message to money MVC to give _model.MoneyOnDeath.Value
    }
}
