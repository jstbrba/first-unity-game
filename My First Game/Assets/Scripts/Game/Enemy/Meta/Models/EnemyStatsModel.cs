using Utilities;
public class EnemyStatsModel : BaseModel
{
    private EnemyStatsConfig _config;
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<int> MoneyOnDeath {  get { return _moneyOnDeath; } }

    private Observable<float> _speed = new Observable<float>();
    private Observable<int> _attack = new Observable<int>();
    private Observable<int> _moneyOnDeath = new Observable<int>();

    public EnemyStatsModel(EnemyStatsConfig config)
    {
        _config = config;
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = _config.Speed;
        _attack.Value = _config.Attack;
        _moneyOnDeath.Value = _config.MoneyOnDeath;
    }
}
