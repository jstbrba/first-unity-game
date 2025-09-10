using UnityEngine;

public class EnemyStatsController : BaseController<EnemyStatsModel, EnemyStatsView>
{
    public EnemyStatsController(EnemyStatsModel model, EnemyStatsView view) : base(model, view) { }

    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.Speed.onValueChanged += Model_Speed_OnValueChanged;
        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;
        _model.MoneyOnDeath.onValueChanged += Model_MoneyOnDeath_OnValueChanged;

        Context.CommandBus.AddListener<DeathCommand>(Health_OnDeath);
        Context.CommandBus.AddListener<SetSpeedCommand>(OnSetSpeed);
        Context.CommandBus.AddListener<SetAttackCommand>(OnSetAttack);
        Context.CommandBus.AddListener<SetMoneyOnDeathCommand>(OnSetMoneyOnDeath);
    }
    public void Model_Speed_OnValueChanged(float previous, float current) 
    {
        Context.CommandBus.Dispatch(new SpeedChangedCommand(previous, current));
    }
    public void Model_Attack_OnValueChanged(int previous, int current) 
    {
        Context.CommandBus.Dispatch(new AttackChangedCommand(previous, current));
    }
    public void Model_MoneyOnDeath_OnValueChanged(int previous, int current) 
    {
        Context.CommandBus.Dispatch(new DeathMoneyChangedCommand(previous, current));
    }
    public void Health_OnDeath(DeathCommand command)
    {
        // send message to money MVC to give _model.MoneyOnDeath.Value
    }
    public void OnSetSpeed(SetSpeedCommand command) => _model.Speed.Value = command.Speed;
    public void OnSetAttack(SetAttackCommand command) => _model.Attack.Value = command.Attack;
    public void OnSetMoneyOnDeath(SetMoneyOnDeathCommand command) => _model.MoneyOnDeath.Value = command.MoneyOnDeath;
}
