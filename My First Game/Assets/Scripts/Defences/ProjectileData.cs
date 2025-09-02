using UnityEngine;

[CreateAssetMenu (fileName = "ProjectileData", menuName = "Projectile/ProjectileData")]
public class ProjectileData : ScriptableObject 
{
    public GameObject ProjectilePrefab;
    public float damage;
    public float speed;
    public float lifetime;

    // maybe add prefabs for FX, or one for effectors for special abilities or smthn
}
