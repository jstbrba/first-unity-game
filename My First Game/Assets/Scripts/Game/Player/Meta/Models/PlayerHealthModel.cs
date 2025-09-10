using Utilities;

public class PlayerHealthModel : BaseModel
{
    public Observable<int> MaxHealth { get { return _maxHealth; } }
    public Observable<int> CurrentHealth { get { return _currentHealth; } }

    private Observable<int> _maxHealth = new Observable<int>();
    private Observable<int> _currentHealth = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        MaxHealth.Value = 20; // make a command later to set it
        CurrentHealth.Value = MaxHealth.Value;
    }
}
