using Utilities;
public class EnemyStatsModel : BaseModel
{
    private EnemyStatsConfig _enemyStatsConfig;
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<int> MoneyOnDeath {  get { return _moneyOnDeath; } }

    private Observable<float> _speed = new Observable<float>();
    private Observable<int> _attack = new Observable<int>();
    private Observable<int> _moneyOnDeath = new Observable<int>();

    public EnemyStatsModel(EnemyStatsConfig enemyStatsConfig)
    {
        _enemyStatsConfig = enemyStatsConfig;
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = _enemyStatsConfig.Speed;
        _attack.Value = _enemyStatsConfig.Attack;
        _moneyOnDeath.Value = _enemyStatsConfig.MoneyOnDeath;
    }
}
