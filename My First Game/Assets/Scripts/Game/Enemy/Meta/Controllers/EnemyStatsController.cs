using UnityEngine;

public class EnemyStatsController : BaseController<EnemyStatsModel, EnemyStatsView>
{
    public EnemyStatsController(EnemyStatsModel model, EnemyStatsView view) : base(model, view) { }

    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        Context.CommandBus.AddListener<DeathCommand>(Health_OnDeath);
    }
    public void Health_OnDeath(DeathCommand command)
    {
        // send message to money MVC to give _model.MoneyOnDeath.Value
    }
}
