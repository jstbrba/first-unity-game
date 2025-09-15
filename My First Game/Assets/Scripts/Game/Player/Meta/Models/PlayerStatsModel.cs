using Utilities;
public class PlayerStatsModel : BaseModel
{
    private PlayerStatsConfig _config;
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }
    public Observable<float> JumpPower { get { return _jumpPower; } }

    private Observable<float> _speed =new Observable<float>();
    private Observable<int> _attack =new Observable<int>();
    private Observable<float> _jumpPower = new Observable<float>();

    public PlayerStatsModel(PlayerStatsConfig config)
    {
        _config = config;
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = _config.Speed;
        _attack.Value = _config.Attack;
        _jumpPower.Value = _config.jumpPower;
    }
}
