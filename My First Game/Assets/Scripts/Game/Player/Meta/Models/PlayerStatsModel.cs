using Utilities;

public class PlayerStatsModel : BaseModel
{
    public Observable<float> Speed { get { return _speed; } }
    public Observable<int> Attack {  get { return _attack; } }

    private Observable<float> _speed =new Observable<float>();
    private Observable<int> _attack =new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _speed.Value = 3;
        _attack.Value = 3;
    }
}
