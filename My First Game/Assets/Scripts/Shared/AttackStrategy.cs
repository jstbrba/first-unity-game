using Game;
using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    [SerializeField] private string animationName;
    [HideInInspector] public int animHash;

    [Header("Attack Settings")]
    [SerializeField] protected int damage;
    [SerializeField] protected float range;
    [SerializeField] protected Vector2 hitBoxSize = new Vector2(1f, 1f);

    [Header("Event Channels")]
    // TODO: Move the money channel somewhere else cos it doesn't make sense for it to be here.
    [SerializeField] private IntEventChannel moneyChannel;

    public float Range => range;
    public Vector2 HitBoxSize => hitBoxSize;

    private void OnEnable()
    {
        animHash = Animator.StringToHash(animationName);
    }
    public abstract void Attack(Transform origin);
    protected void EnemyDetection(Collider2D[] hits)
    {
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Health health = hit.GetComponent<Health>();
                if (health != null) health.TakeDamage(damage);
                if (health.CurrentHealth == 0) moneyChannel.Invoke(hit.GetComponent<Enemy>().MoneyOnDeath);
            }
        }
    }
    public virtual void DrawGizmos(Transform origin)
    {
        // noop
    }
}
