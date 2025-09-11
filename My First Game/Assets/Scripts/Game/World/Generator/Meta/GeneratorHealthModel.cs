using Utilities;
public class GeneratorHealthModel : BaseModel
{
    public Observable<int> MaxHealth { get { return _maxHealth; } }
    public Observable<int> CurrentHealth { get { return _currentHealth; } }

    private Observable<int> _maxHealth = new Observable<int>();
    private Observable<int> _currentHealth = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _maxHealth.Value = 70;
        _currentHealth.Value = _maxHealth.Value;
    }
}
