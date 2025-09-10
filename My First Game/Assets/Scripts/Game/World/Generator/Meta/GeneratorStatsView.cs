using UnityEngine;
using UnityEngine.UI;

public class GeneratorStatsView : MonoBehaviour, IView
{
    public IContext Context { get {  return _context; } }
    private IContext _context;

    [SerializeField] private Image _powerBar;
    private int _maxPower;

    public void Initialise(IContext context)
    {
        _context = context;

        _context.CommandBus.AddListener<PowerChangedCommand>(OnPowerChanged);
        _context.CommandBus.AddListener<MaxPowerChangedCommand>(OnMaxPowerChanged);
    }

    private void OnPowerChanged(PowerChangedCommand command)
    {
        _powerBar.fillAmount = (float) command.Current / _maxPower;
        Debug.Log("View received power change!");
    }
    private void OnMaxPowerChanged(MaxPowerChangedCommand command)
    {
        _maxPower = command.Current;
    }
}
