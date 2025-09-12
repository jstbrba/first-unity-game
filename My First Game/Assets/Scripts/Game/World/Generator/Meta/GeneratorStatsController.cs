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

        Context.CommandBus.AddListener<ApplyChargeCommand>(ApplyCharge);
    }
    private void Model_CurrentPower_OnValueChanged(int previous, int current)
    {
        if (_model.CurrentPower.Value == 0)
            Context.CommandBus.Dispatch(new PowerDownCommand());
    }
    private void ApplyCharge(ApplyChargeCommand command)
    {
        _model.CurrentPower.Value = Mathf.Clamp(_model.CurrentPower.Value + command.Charge, 0, _model.MaxPower.Value);
    }
}