using UnityEngine;

public class MoneyView : MonoBehaviour 
{
    [SerializeField] private TMPro.TextMeshProUGUI _moneyText;
    private IContext _context;
    private int _startingValue;

    public void Initialise(IContext context, int startingValue)
    {
        _context = context;
        _startingValue = startingValue;

        _context.CommandBus.AddListener<MoneyChangedCommand>(OnMoneyChanged);
    }
    public void OnMoneyChanged(MoneyChangedCommand command)
    {
        _moneyText.text = "$$$: " + command.Current;
    }
}
