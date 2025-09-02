using UnityEngine;

public class ProjectileFactory 
{
    public GameObject CreateProjectile(ProjectileData data, Transform firePoint)
    {
        Projectile.Builder builder = new Projectile.Builder()
            .SetBasePrefab(data.ProjectilePrefab)
            .SetDamage(data.damage)
            .SetSpeed(data.speed)
            .SetLifetime(data.lifetime)
            .SetFirePoint(firePoint);

        return builder.Build();
    }
}
