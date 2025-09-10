using UnityEngine;

public class GeneratorStatsController : BaseController<GeneratorStatsModel, GeneratorStatsView>
{
    public GeneratorStatsController(GeneratorStatsModel model, GeneratorStatsView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.CurrentPower.onValueChanged += Model_CurrentPower_OnValueChanged;
        _model.MaxPower.onValueChanged += Model_MaxPower_OnValueChanged;

        Model_MaxPower_OnValueChanged(0, _model.MaxPower.Value);

        Context.CommandBus.AddListener<ApplyChargeCommand>(ApplyCharge);
    }
    private void Model_CurrentPower_OnValueChanged(int previous, int current)
    {
        Context.CommandBus.Dispatch(new PowerChangedCommand(previous, current));

        if (_model.CurrentPower.Value == 0)
            Context.CommandBus.Dispatch(new PowerDownCommand());
    }
    private void Model_MaxPower_OnValueChanged(int previous, int current) 
    {
        Context.CommandBus.Dispatch(new MaxPowerChangedCommand(previous, current));
    }
    private void ApplyCharge(ApplyChargeCommand command)
    {
        _model.CurrentPower.Value = Mathf.Clamp(_model.CurrentPower.Value + command.Charge, 0, _model.MaxPower.Value);
    }
}