using UnityEngine;

public class ProjectileFactory 
{
    public GameObject CreateProjectile(ProjectileData data, Transform firePoint, int direction)
    {
        Projectile.Builder builder = new Projectile.Builder()
            .SetBasePrefab(data.ProjectilePrefab)
            .SetDamage(data.damage)
            .SetSpeed(data.speed)
            .SetLifetime(data.lifetime)
            .SetFirePoint(firePoint)
            .SetDirection(direction);

        return builder.Build();
    }
}
