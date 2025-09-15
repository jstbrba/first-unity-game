using Utilities;

public class EnemySpawnerModel : BaseModel
{
    public Observable<int> SpawnLimit { get { return _spawnLimit; } }
    public Observable<int> SpawnCount { get { return _spawnCount; } }
    public Observable<int> KillCount { get { return _killCount; } }
    public Observable<bool> AllEnemiesDead { get {  return _allEnemiesDead; } }

    private Observable<int> _spawnLimit = new Observable<int>();
    private Observable<int> _spawnCount = new Observable<int>();
    private Observable<int> _killCount = new Observable<int>();
    private Observable<bool> _allEnemiesDead = new Observable<bool>();

    public override void Initialise(IContext context)
    {
        base.Initialise(context);
        _spawnLimit.Value = 10;
        _spawnCount.Value = 0;
        _killCount.Value = 0;
        _allEnemiesDead.Value = false;
    }
}
