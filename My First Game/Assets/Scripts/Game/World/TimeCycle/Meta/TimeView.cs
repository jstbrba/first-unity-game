using Game;
using TMPro;
using UnityEngine;

public class TimeView : MonoBehaviour, IView
{
    [SerializeField] private TextMeshProUGUI _dayNightText;
    [SerializeField] private TextMeshProUGUI _dayCounterText;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private EnemySpawner _enemySpawner;

    private IContext _context;
    private TimeModel _model;

    private readonly Color _dayColour = new Color(127f / 255f, 196f / 255f, 245f / 255f);
    private readonly Color _nightColour = new Color(23f / 255f, 36f / 255f, 66f / 255f);
    private Color _targetColour;
    private float _lerpSpeed = 0.2f;
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<TimeModel>();

        SetDayCounterText(_model.Day.Value);
        SetDaytimeText(_model.IsDayTime.Value);
        SetDaytimeTextColour(_model.CanSleep.Value);
        SetColour(_model.IsDayTime.Value);

        _model.Day.onValueChanged += Model_Day_OnValueChanged;
        _model.IsDayTime.onValueChanged += Model_IsDayTime_OnValueChanged;
        _model.CanSleep.onValueChanged += Model_CanSleep_OnValueChanged;
    }

    private void Model_Day_OnValueChanged(int previous, int current)
    {
        SetDayCounterText(current);
    }
    private void Model_IsDayTime_OnValueChanged(bool previous, bool current)
    {
        SetDaytimeText(current);
        SetColour(current);
        if (!current) _enemySpawner.DespawnEnemiesOutOfView();
    }
    private void Model_CanSleep_OnValueChanged(bool previous, bool current)
    {
        SetDaytimeTextColour(current);
    }
    private void SetDayCounterText(int current)
    {
        _dayCounterText.text = "Day: " + current;
    }
    private void SetDaytimeText(bool current)
    {
        _dayNightText.text = current ? "Daytime" : "Nighttime";
    }
    private void SetDaytimeTextColour(bool current)
    {
        _dayNightText.color = current ? Color.green : Color.white;
    }
    private void SetColour(bool current)
    {
        _targetColour = current ? _dayColour : _nightColour;
    }
    private void Update()
    {
        _background.color = _model.IsDayTime.Value ? _targetColour : Color.Lerp(_background.color, _targetColour, _lerpSpeed * Time.deltaTime);
    }
}
