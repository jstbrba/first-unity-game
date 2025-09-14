using UnityEngine;

public class UpgradeMenuController : BaseController<UpgradeMenuModel, UpgradeMenuView>
{
    public UpgradeMenuController(UpgradeMenuModel model, UpgradeMenuView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        Context.CommandBus.AddListener<ToggleMenuCommand>(OnToggleMenu);
        Context.CommandBus.AddListener<PurchaseResponse>(OnPurchaseResponse);

        _view.OnSpeedUpgrade += View_OnSpeedUpgrade;
        _view.OnAttackUpgrade += View_OnAttackUpgrade;
        _view.OnDoorUpgrade += View_OnDoorUpgrade;
        _view.OnGenUpgrade += View_OnGenUpgrade;
    }
    public void OnToggleMenu(ToggleMenuCommand command) => _model.IsMenuOpen.Value = !_model.IsMenuOpen.Value;

    public void View_OnSpeedUpgrade() 
    {
        foreach (var moneyContext in ContextLocator.Get<EconomyContext>()) 
        {
            moneyContext.CommandBus.Dispatch(new PurchaseRequest(PurchaseType.SpeedUpgrade,_model.SpeedUpgradePrice.Value, Context));
        }
    }
    public void View_OnAttackUpgrade() 
    {
        // Send request to money MVC and wait for response
        // Send upgrade command to other context
        _model.AttackUpgradePrice.Value = Mathf.Min((int)(_model.AttackUpgradePrice.Value * 1.5f), 99999);
    }
    public void View_OnDoorUpgrade() 
    {
        foreach (var moneyContext in ContextLocator.Get<EconomyContext>())
        {
            moneyContext.CommandBus.Dispatch(new PurchaseRequest(PurchaseType.DoorUpgrade, _model.DoorUpgradePrice.Value, Context));
        }
    }
    public void View_OnGenUpgrade() 
    {
        foreach (var moneyContext in ContextLocator.Get<EconomyContext>())
        {
            moneyContext.CommandBus.Dispatch(new PurchaseRequest(PurchaseType.GenUpgrade, _model.GenUpgradePrice.Value, Context));
        }
    }
    public void OnPurchaseResponse(PurchaseResponse response)
    {
        if (!response.Success)
        {
            return;
        }

        PurchaseType type = response.PurchaseType;
        switch (type)
        {
            case PurchaseType.SpeedUpgrade:
                DispatchToPlayer(new UpgradeSpeedCommand(1));
                _model.SpeedUpgradePrice.Value = Mathf.Min((int)(_model.SpeedUpgradePrice.Value * 1.5f), 99999);
                return;
            case PurchaseType.AttackUpgrade:
                return;
            case PurchaseType.DoorUpgrade:
                DispatchToShutter(new UpgradeMaxHealthCommand(5));
                _model.DoorUpgradePrice.Value = Mathf.Min((int)(_model.DoorUpgradePrice.Value * 1.5f), 99999);
                return;
            case PurchaseType.GenUpgrade:
                DispatchToGenerator(new UpgradeMaxHealthCommand(5));
                _model.GenUpgradePrice.Value = Mathf.Min((int)(_model.GenUpgradePrice.Value * 1.5f), 99999);
                return;
        }
    }
    public void DispatchToPlayer(ICommand command)
    {
        foreach (var ctx in ContextLocator.Get<PlayerContext>())
            ctx.CommandBus.Dispatch(command);
    }
    public void DispatchToShutter(ICommand command)
    {
        foreach (var ctx in ContextLocator.Get<ShutterContext>())
            ctx.CommandBus.Dispatch(command);
    }
    public void DispatchToGenerator(ICommand command)
    {
        foreach (var ctx in ContextLocator.Get<GeneratorContext>())
            ctx.CommandBus.Dispatch(command);
    }
}
