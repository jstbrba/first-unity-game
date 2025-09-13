using UnityEngine;

public class MoneyController : BaseController<MoneyModel, MoneyView>
{

    public MoneyController(MoneyModel model, MoneyView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        Context.CommandBus.AddListener<PurchaseRequest>(OnPurchaseRequest);
    }
    public void OnPurchaseRequest(PurchaseRequest request)
    {
        PurchaseType type = request.PurchaseType;
        int requiredAmount = request.RequiredAmount;
        IContext returnContext = request.ReturnContext;

        if (_model.Money.Value < requiredAmount)
        {
            returnContext.CommandBus.Dispatch(new PurchaseResponse(type, false));
        } else
        {
            returnContext.CommandBus.Dispatch(new PurchaseResponse(type, true));
            RemoveMoney(requiredAmount);
        }
    }
    public void AddMoney(int amount)
    {
        _model.Money.Value += amount;
    }
    public void RemoveMoney(int amount) 
    {
        _model.Money.Value -= amount;
    }
}
