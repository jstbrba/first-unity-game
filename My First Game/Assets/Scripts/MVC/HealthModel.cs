using UnityEngine;
using Utilities;
public class HealthModel 
{
    public Observable<int> Health { get; } = new Observable<int>();

    public void Initialise(int startingHealth)
    {
        Health.Value = startingHealth;
    }
    public void TakeDamage(int damage)
    {
        Health.Value = Mathf.Max(0, Health.Value - damage);
    }
}