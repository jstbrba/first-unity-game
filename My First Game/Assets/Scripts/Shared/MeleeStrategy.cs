using UnityEngine;
[CreateAssetMenu(fileName = "MeleeStrategy", menuName = "Attacks/MeleeStrategy")]
public class MeleeStrategy : AttackStrategy
{
    public override void Attack(Transform origin)
    {
        float direction = Mathf.Sign(origin.localScale.x);

        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(origin.position.x + direction * range, origin.position.y), HitBoxSize, 0f);

        EnemyDetection(hits);
    }
    public override void DrawGizmos(Transform origin)
    {
        float direction = Mathf.Sign(origin.localScale.x);
        Vector2 center = new Vector2(origin.position.x + direction * range, origin.position.y);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, HitBoxSize);
    }
}
