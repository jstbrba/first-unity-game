using UnityEngine;
using Utilities;

public class EnemySpawnerController : BaseController<EnemySpawnerModel, EnemySpawnerView>
{
    private CountdownTimer _spawnTimer;
    public EnemySpawnerController(EnemySpawnerModel model, EnemySpawnerView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _spawnTimer = new CountdownTimer(5f);
        _spawnTimer.Start();

        _model.KillCount.onValueChanged += Model_KillCount_OnValueChanged;

        Context.CommandBus.AddListener<DeathCommand>(OnEnemyDeath);
        Context.CommandBus.AddListener<OnDayCompleteCommand>(OnDayComplete);
    }
    public void Update(float deltaTime)
    {
        _spawnTimer.Tick(deltaTime);

        if (_spawnTimer.IsFinished && !IsLimitReached)
        {
            Debug.Log("Controller increased model spawn count");
            _model.SpawnCount.Value++;
            _spawnTimer.Start();
        }
    }
    private void Model_KillCount_OnValueChanged(int previous, int current)
    {
        if (current == _model.SpawnLimit.Value)
        {
            _model.AllEnemiesDead.Value = true;
            NotifyTimeCycle(new OnWaveCompleteCommand());
        }
    }
    private void OnEnemyDeath(DeathCommand command)
    {
        _model.KillCount.Value++;
    }
    private void OnDayComplete(OnDayCompleteCommand command)
    {
        _model.KillCount.Value = 0;
        _model.SpawnCount.Value = 0;
        _model.SpawnLimit.Value += 10;
    }
    private void NotifyTimeCycle(ICommand command)
    {
        foreach (var ctx in ContextLocator.Get<TimeContext>())
            ctx.CommandBus.Dispatch(command);
    }
    private bool IsLimitReached => _model.SpawnCount.Value == _model.SpawnLimit.Value;
}
