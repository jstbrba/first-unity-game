using UnityEngine;
using Utilities;
[CreateAssetMenu(fileName = "EnemyStats", menuName = "Models/Enemy/EnemyStats")]
public class EnemyStatsModel : BaseModel
{
    [SerializeField] private float _baseSpeed = 1f;
    [SerializeField] private int _baseAttack = 2;
    [SerializeField] private int _baseMoneyOnDeath = 20;
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<int> MoneyOnDeath {  get { return _moneyOnDeath; } }

    private Observable<float> _speed = new Observable<float>();
    private Observable<int> _attack = new Observable<int>();
    private Observable<int> _moneyOnDeath = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = _baseSpeed;
        _attack.Value = _baseAttack;
        _moneyOnDeath.Value = _baseMoneyOnDeath;
    }
}
