using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _dayNightText;
    [SerializeField] private TextMeshProUGUI _dayCounterText;

    private IContext _context;
    private TimeModel _model;

    private int _day;
    private bool _isDayTime;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<TimeModel>();

        InitialiseDayCounterText(_model.Day.Value);
        InitialiseDaytimeText(_model.IsDayTime.Value);

        _model.Day.onValueChanged += Model_Day_OnValueChanged;
        _model.IsDayTime.onValueChanged += Model_IsDayTime_OnValueChanged;
    }

    private void Model_Day_OnValueChanged(int previous, int current)
    {
        _day = current;
        InitialiseDayCounterText(current);
    }
    private void Model_IsDayTime_OnValueChanged(bool previous, bool current)
    {
        _isDayTime = current;
        InitialiseDaytimeText(current);
    }
    private void InitialiseDayCounterText(int day)
    {
        _dayCounterText.text = "Day: " + day;
    }
    private void InitialiseDaytimeText(bool isDay)
    {
        if (isDay)
            _dayNightText.text = "Daytime";
        else
            _dayNightText.text = "Nighttime";
    }
}
