using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // TODO : Move all this into MVC cos all this really does is just Invoke OnPowerDown()
    private IContext _context;
    private GeneratorStatsModel _statsModel;

    private int _maxPower;
    private int _currentPower;
    public event Action OnPowerDown;
    public void Initialise(IContext context)
    {
        _context = context;

        _statsModel = _context.ModelLocator.Get<GeneratorStatsModel>();
        _maxPower = _statsModel.MaxPower.Value;
        _currentPower = _statsModel.CurrentPower.Value;

        _statsModel.MaxPower.onValueChanged += Model_MaxPower_OnValueChanged;
        _statsModel.CurrentPower.onValueChanged -= Model_CurrentPower_OnValueChanged;
        Debug.Log("Gen Max power: " + _maxPower);

        _context.CommandBus.AddListener<DeathCommand>(HandleDeath);
        _context.CommandBus.AddListener<PowerDownCommand>(HandlePowerDown);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //_context.CommandBus.Dispatch(new ApplyChargeCommand(-2));
            _context.CommandBus.Dispatch(new ApplyDamageCommand(2));
        }
    }
    public void HandleDeath(DeathCommand command)
    {
        OnPowerDown?.Invoke();
        gameObject.SetActive(false);
    }
    public void HandlePowerDown(PowerDownCommand command) 
    {
        OnPowerDown.Invoke();
    }
    private void Model_MaxPower_OnValueChanged(int previous, int current) => _maxPower = current;
    private void Model_CurrentPower_OnValueChanged(int previous, int current) => _currentPower = current;
}
