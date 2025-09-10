using Utilities;

public class GeneratorStatsModel : BaseModel 
{
    public Observable<int> CurrentPower { get { return _currentPower; } }
    public Observable<int> MaxPower { get { return _maxPower; } }
    private Observable<int> _currentPower = new Observable<int>();
    private Observable<int> _maxPower = new Observable<int>();

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _maxPower.Value = 100;
        _currentPower.Value = _maxPower.Value;
    }
}
