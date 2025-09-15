using Utilities;
public class TimeModel : BaseModel
{
    public Observable<bool> IsDayTime { get { return _isDayTIme; } }
    public Observable<int> Day { get {  return _day; } }
    public Observable<bool> CanSleep { get { return _canSleep; } }
    public float DayDuration { get { return _dayDuration; } }
    public float NightDuration { get { return _nightDuration; } }

    private Observable<bool> _isDayTIme = new Observable<bool>();
    private Observable<int> _day = new Observable<int>();
    private Observable<bool> _canSleep = new Observable<bool>();
    private float _dayDuration;
    private float _nightDuration;

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _day.Value = 1;
        _isDayTIme.Value = true;
        _canSleep.Value = false;
        _dayDuration = 10;
        _nightDuration = 10;
    }
}
