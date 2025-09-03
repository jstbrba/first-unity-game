using UnityEngine;

[CreateAssetMenu(fileName = "ProjecileSettings", menuName = "Flyweight/ProjectileSettings")]
public class ProjectileSettings : FlyweightSettings
{
    public float damage;
    public float despawnDelay;
    public float speed;

    public override Flyweight Create()
    {
        var go = Instantiate(prefab);
        go.SetActive(false);
        go.name = prefab.name;

        var flyweight = go.AddComponent<Projectile>();
        flyweight.settings = this;
        return flyweight;
    }
}

