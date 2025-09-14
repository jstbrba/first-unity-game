using Utilities;
public class TimeModel : BaseModel
{
    public Observable<bool> IsDayTime { get { return _isDayTIme; } }
    public Observable<int> Day { get {  return _day; } }

    private Observable<bool> _isDayTIme = new Observable<bool>();
    private Observable<int> _day = new Observable<int>();

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _day.Value = 1;
        _isDayTIme.Value = true;
    }
}
