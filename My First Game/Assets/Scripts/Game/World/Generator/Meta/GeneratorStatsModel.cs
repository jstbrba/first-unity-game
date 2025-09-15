using Utilities;
public class GeneratorStatsModel : BaseModel 
{
    private GeneratorStatsConfig _config;
    public Observable<int> CurrentPower { get { return _currentPower; } }
    public Observable<int> MaxPower { get { return _maxPower; } }
    private Observable<int> _currentPower = new Observable<int>();
    private Observable<int> _maxPower = new Observable<int>();

    public GeneratorStatsModel(GeneratorStatsConfig config)
    {
        _config = config;
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _maxPower.Value = _config.MaxPower;
        _currentPower.Value = _maxPower.Value;
    }
}
