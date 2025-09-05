using UnityEngine;
using Utilities;
public class HealthModel 
{
    public int MaxHealth { get { return _maxHealth; } } // Change to observable if i allow max health to be upgraded
    public Observable<int> Health { get { return _health; } }

    private int _maxHealth;
    private Observable<int> _health = new Observable<int>();

    public void Initialise(int startingHealth)
    {
        _maxHealth = startingHealth;
        _health.Value = _maxHealth;
    }
}