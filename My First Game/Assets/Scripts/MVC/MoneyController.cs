public class MoneyController 
{
    private readonly MoneyModel _model;
    private readonly IContext _context;

    public MoneyController(MoneyModel model, IContext context)
    {
        _model = model;
        _context = context;
    }
    public void Initialise()
    {
        _model.Money.onValueChanged += Model_OnMoneyChanged;
    }
    public void AddMoney(int amount)
    {
        _model.Money.Value += amount;
    }
    public void Model_OnMoneyChanged(int previous, int current)
    {
        _context.CommandBus.Dispatch(new MoneyChangedCommand(previous, current));
    }
}
