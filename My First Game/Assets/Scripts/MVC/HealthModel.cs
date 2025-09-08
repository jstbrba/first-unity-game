using UnityEngine;
using Utilities;
public class HealthModel 
{
    public Observable<int> MaxHealth { get { return _maxHealth; } }
    public Observable<int> Health { get { return _health; } }

    private Observable<int> _maxHealth = new Observable<int>();
    private Observable<int> _health = new Observable<int>();

    public void Initialise(int startingHealth)
    {
        _maxHealth.Value = startingHealth;
        _health.Value = _maxHealth.Value;
    }
}