using Game;
using System.Threading;
using UnityEngine;

public class TimeController : BaseController<TimeModel, TimeView>
{
    private StateMachine _stateMachine;
    private DayState _dayState;
    private NightState _nightState;

    private bool _enemySpawner_allEnemiesDead; // probably gonna change
    public TimeController(TimeModel model, TimeView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _enemySpawner_allEnemiesDead = false;

        _stateMachine = new StateMachine();

        _dayState = new DayState(this, _model.DayDuration);
        _nightState = new NightState(this, _model.NightDuration);

        _stateMachine.AddTransition(_dayState, _nightState, new FuncPredicate(() => _dayState.IsFinished));
        _stateMachine.SetState(_dayState);

        Context.CommandBus.AddListener<OnSleepCommand>(OnSleep);
        Context.CommandBus.AddListener<OnWaveCompleteCommand>(OnWaveComplete);
    }
    public void SetTimeDay() => _model.IsDayTime.Value = true;
    public void SetTimeNight() => _model.IsDayTime.Value = false;
    public void SetCanSleep(bool newValue) => _model.CanSleep.Value = newValue;
    public void Update() => _stateMachine.Update();
    public void FixedUpdate() => _stateMachine.FixedUpdate();
    private void OnSleep(OnSleepCommand command)
    {
        if (!_enemySpawner_allEnemiesDead) Debug.Log("There are still enemies left!");
        if (_model.CanSleep.Value && _enemySpawner_allEnemiesDead && _stateMachine.GetCurrentState() == _nightState)
        {
            _stateMachine.SetState(_dayState);
            _model.Day.Value++;
            _enemySpawner_allEnemiesDead = false;
            NotifySpawner(new OnDayCompleteCommand());
        }
    }
    private void OnWaveComplete(OnWaveCompleteCommand command) => _enemySpawner_allEnemiesDead = true;
    private void NotifySpawner(ICommand command)
    {
        foreach(var ctx in ContextLocator.Get<EnemySpawnerContext>())
            ctx.CommandBus.Dispatch(command);
    }
}