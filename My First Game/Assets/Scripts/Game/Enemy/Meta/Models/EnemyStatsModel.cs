using Utilities;
public class EnemyStatsModel : BaseModel
{
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<int> MoneyOnDeath {  get { return _moneyOnDeath; } }

    private Observable<float> _speed = new Observable<float>();
    private Observable<int> _attack = new Observable<int>();
    private Observable<int> _moneyOnDeath = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = 1f;
        _attack.Value = 2;
        _moneyOnDeath.Value = 20;
    }
}
