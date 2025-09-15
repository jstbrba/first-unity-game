public class EnemySpawnerController : BaseController<EnemySpawnerModel, EnemySpawnerView>
{
    public EnemySpawnerController(EnemySpawnerModel model, EnemySpawnerView view) : base(model, view)
    {
    }
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _model.KillCount.onValueChanged += Model_KillCount_OnValueChanged;
    }
    private void Model_KillCount_OnValueChanged(int previous, int current)
    {
        if (current == _model.SpawnLimit.Value) _model.AllEnemiesDead.Value = true;
    }
}
