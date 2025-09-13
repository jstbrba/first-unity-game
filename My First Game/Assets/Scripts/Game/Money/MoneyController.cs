public class MoneyController : BaseController<MoneyModel, MoneyView>
{

    public MoneyController(MoneyModel model, MoneyView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);
    }
    public void AddMoney(int amount)
    {
        _model.Money.Value += amount;
    }
}
