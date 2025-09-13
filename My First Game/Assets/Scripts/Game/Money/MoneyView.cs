using UnityEngine;

public class MoneyView : MonoBehaviour 
{
    [SerializeField] private TMPro.TextMeshProUGUI _moneyText;
    private IContext _context;
    private MoneyModel _model;
    private int _money;

    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<MoneyModel>();
        _money = _model.Money.Value;

        _model.Money.onValueChanged += Model_Money_OnValueChanged;
    }
    public void Model_Money_OnValueChanged(int previous, int current)
    {
        _money = current;
        _moneyText.text = "$$$: " + current;
    }
}
