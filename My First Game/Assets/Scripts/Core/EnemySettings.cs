using Game;
using UnityEngine;
[CreateAssetMenu (fileName = "EnemySettings", menuName = "Flyweight/EnemySettings")]
public class EnemySettings : FlyweightSettings 
{
    public int maxHealth;
    public float speed;
    public int moneyOnDeath;
    public IntEventChannel moneyChannel;

    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        // var flyweight = go.AddComponent<Enemy>();
        var flyweight = go.GetComponent<Enemy>();
        flyweight.settings = this;
        // go.GetComponent<Health>().Controller.SetMaxHealth(maxHealth);
        return flyweight;
    }
}

