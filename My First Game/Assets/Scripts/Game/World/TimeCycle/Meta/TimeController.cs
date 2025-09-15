using Game;
using UnityEngine;

public class TimeController : BaseController<TimeModel, TimeView>
{
    private StateMachine _stateMachine;
    private DayState _dayState;
    private NightState _nightState;
    public TimeController(TimeModel model, TimeView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _stateMachine = new StateMachine();

        _dayState = new DayState(this, _model.DayDuration);
        _nightState = new NightState(this, _model.NightDuration);

        _stateMachine.AddTransition(_dayState, _nightState, new FuncPredicate(() => _dayState.IsFinished));
        _stateMachine.SetState(_dayState);

        Context.CommandBus.AddListener<OnSleepCommand>(OnSleepCommand);
    }
    public void SetTimeDay() => _model.IsDayTime.Value = true;
    public void SetTimeNight() => _model.IsDayTime.Value = false;
    public void SetCanSleep(bool newValue) => _model.CanSleep.Value = newValue;
    public void Update() => _stateMachine.Update();
    public void FixedUpdate() => _stateMachine.FixedUpdate();
    private void OnSleepCommand(OnSleepCommand command)
    {
        if (_nightState.IsFinished && _stateMachine.GetCurrentState() == _nightState)
        {
            _stateMachine.SetState(_dayState);
            _model.Day.Value++;
        }
    }
}