using Utilities;
using UnityEngine;
[CreateAssetMenu(fileName = "ShutterHealth", menuName = "Models/Shutter/ShutterHealth")]
public class ShutterHealthModel : BaseModel
{
    [SerializeField] private int _baseMaxHealth = 10;
    public Observable<int> MaxHealth { get { return _maxHealth; } }
    public Observable<int> CurrentHealth { get { return _currentHealth; } }

    private Observable<int> _maxHealth = new Observable<int>();
    private Observable<int> _currentHealth = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        _maxHealth.Value = _baseMaxHealth;
        _currentHealth.Value = _maxHealth.Value;
    }
}
