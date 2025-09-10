using System;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private IContext _context;

    [SerializeField] private int maxPower;
    private int currentPower;
    public event Action OnPowerDown;

    private void Start()
    {
        currentPower = maxPower;
    }
    public void Initialise(IContext context)
    {
        _context = context;

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
    public void Repair(int value) => currentPower = Mathf.Clamp(currentPower + value, 0, maxPower);
    public void HandleDeath(DeathCommand command)
    {
        OnPowerDown?.Invoke();
        gameObject.SetActive(false);
    }
    public void HandlePowerDown(PowerDownCommand command) 
    {
        OnPowerDown.Invoke();
    }
}
