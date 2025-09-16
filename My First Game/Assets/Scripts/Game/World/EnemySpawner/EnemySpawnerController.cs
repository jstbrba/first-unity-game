public class EnemySpawnerController : BaseController<EnemySpawnerModel, EnemySpawnerView>
{
    public EnemySpawnerController(EnemySpawnerModel model, EnemySpawnerView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.KillCount.onValueChanged += Model_KillCount_OnValueChanged;

        Context.CommandBus.AddListener<DeathCommand>(OnEnemyDeath);
        Context.CommandBus.AddListener<OnDayCompleteCommand>(OnDayComplete);
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
}
