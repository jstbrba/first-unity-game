using UnityEngine;
[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Attacks/MeleeStrategy")]
public class MeleeStrategy : AttackStrategy
{
    [Header("Attack Settings")]
    [SerializeField] public float damage;
    [SerializeField] private float range;
    [SerializeField] private Vector2 hitBoxSize = new Vector2(1f,1f);

    public float Damage => damage;
    public float Range => range;
    public Vector2 HitBoxSize => hitBoxSize;


    public override void Attack(Transform origin)
    {
        Debug.Log("Performing light attack");
        float direction = Mathf.Sign(origin.localScale.x);

        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(origin.position.x + direction * range, origin.position.y), HitBoxSize, 0f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Health health = hit.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
    public override void DrawGizmos(Transform origin)
    {
        float direction = Mathf.Sign(origin.localScale.x);
        Vector2 center = new Vector2(origin.position.x + direction * range, origin.position.y);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, HitBoxSize);
    }
}
