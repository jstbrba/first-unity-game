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

        _view.OnSpeedUpgrade += View_OnSpeedUpgrade;
        _view.OnAttackUpgrade += View_OnAttackUpgrade;
        _view.OnDoorUpgrade += View_OnDoorUpgrade;
        _view.OnGenUpgrade += View_OnGenUpgrade;
    }
    public void OnToggleMenu(ToggleMenuCommand command) => _model.IsMenuOpen.Value = !_model.IsMenuOpen.Value;

    public void View_OnSpeedUpgrade() 
    {
        // Send request to money MVC and wait for response
        // Send upgrade command to other context
        _model.SpeedUpgradePrice.Value = Mathf.Min((int)(_model.SpeedUpgradePrice.Value * 1.5f),99999);
    }
    public void View_OnAttackUpgrade() 
    {
        // Send request to money MVC and wait for response
        // Send upgrade command to other context
        _model.AttackUpgradePrice.Value = Mathf.Min((int)(_model.AttackUpgradePrice.Value * 1.5f), 99999);
    }
    public void View_OnDoorUpgrade() 
    {
        // Send request to money MVC and wait for response
        // Send upgrade command to other context
        _model.DoorUpgradePrice.Value = Mathf.Min((int)(_model.DoorUpgradePrice.Value * 1.5f), 99999);
    }
    public void View_OnGenUpgrade() 
    {
        // Send request to money MVC and wait for response
        // Send upgrade command to other context
        _model.GenUpgradePrice.Value = Mathf.Min((int)(_model.GenUpgradePrice.Value * 1.5f), 99999);
    }
}
