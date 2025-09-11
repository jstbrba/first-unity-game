using UnityEngine;
using Utilities;
[CreateAssetMenu(fileName = "PlayerHealth", menuName = "Models/Player/PlayerHealth")]
public class PlayerHealthModel : BaseModel
{
    [SerializeField] private int _baseMaxHealth;
    public Observable<int> MaxHealth { get { return _maxHealth; } }
    public Observable<int> CurrentHealth { get { return _currentHealth; } }

    private Observable<int> _maxHealth = new Observable<int>();
    private Observable<int> _currentHealth = new Observable<int>();
    public override void Initialise(IContext context)
    {
        base.Initialise(context);

        MaxHealth.Value = _baseMaxHealth;
        CurrentHealth.Value = MaxHealth.Value;
    }
}
