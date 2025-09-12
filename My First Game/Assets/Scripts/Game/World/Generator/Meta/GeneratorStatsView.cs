using UnityEngine;
using UnityEngine.UI;

public class GeneratorStatsView : MonoBehaviour, IView
{
    public IContext Context { get {  return _context; } }
    private IContext _context;
    private GeneratorStatsModel _model;

    [SerializeField] private Image _powerBar;
    private int _currentPower;
    private int _maxPower;

    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<GeneratorStatsModel>();

        InitialiseValues();

        _model.CurrentPower.onValueChanged += Model_CurrentPower_OnValueChanged;
        _model.MaxPower.onValueChanged -= Model_MaxPower_OnValueChanged;
    }

    private void Model_CurrentPower_OnValueChanged(int previous, int current)
    {
        _currentPower = current;
        _powerBar.fillAmount = (float) _currentPower / _maxPower;
    }
    private void Model_MaxPower_OnValueChanged(int previous, int current)
    {
        _maxPower = current;
    }
    private void InitialiseValues()
    {
        _currentPower = _model.CurrentPower.Value;
        _maxPower = _model.MaxPower.Value;
        _powerBar.fillAmount = (float) _currentPower / _maxPower;
    }
}
