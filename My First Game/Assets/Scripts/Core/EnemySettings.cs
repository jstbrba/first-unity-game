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

        var flyweight = go.AddComponent<Enemy>();
        flyweight.settings = this;
        go.GetComponent<Health>().SetMaxHealth(maxHealth);
        return flyweight;
    }
}

