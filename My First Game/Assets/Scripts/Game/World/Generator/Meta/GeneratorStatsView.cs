using UnityEngine;
using UnityEngine.UI;

public class GeneratorStatsView : MonoBehaviour, IView
{
    public IContext Context { get {  return _context; } }
    private IContext _context;
    private GeneratorStatsModel _model;

    [SerializeField] private Image _powerBar;

    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<GeneratorStatsModel>();

        UpdateFillAmount();

        _model.CurrentPower.onValueChanged += Model_CurrentPower_OnValueChanged;
        _model.MaxPower.onValueChanged -= Model_MaxPower_OnValueChanged;
    }

    private void Model_CurrentPower_OnValueChanged(int previous, int current)
    {
        UpdateFillAmount();
    }
    private void Model_MaxPower_OnValueChanged(int previous, int current)
    {
        UpdateFillAmount();
    }
    private void UpdateFillAmount()
    {
        _powerBar.fillAmount = (float)_model.CurrentPower.Value / _model.MaxPower.Value;
    }
    private void HandleDeath() => gameObject.SetActive(false);
}
