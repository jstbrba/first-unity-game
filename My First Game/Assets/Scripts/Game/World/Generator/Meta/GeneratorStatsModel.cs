using UnityEngine;
using Utilities;
[CreateAssetMenu(fileName = "GeneratorStats", menuName = "Models/Generator/GeneratorStats")]
public class GeneratorStatsModel : BaseModel 
{
    [SerializeField] private int _baseMaxPower;
    public Observable<int> CurrentPower { get { return _currentPower; } }
    public Observable<int> MaxPower { get { return _maxPower; } }
    private Observable<int> _currentPower = new Observable<int>();
    private Observable<int> _maxPower = new Observable<int>();

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _maxPower.Value = _baseMaxPower;
        _currentPower.Value = _maxPower.Value;
    }
}
