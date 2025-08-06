using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Vector2 hitBoxSize;

    [SerializeField] private LayerMask enemyLayer;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && cooldownTimer > cooldown)
            anim.SetTrigger("attack");
    }

    private void Attack()
    {
        Vector2 position = (Vector2)transform.position + new Vector2(transform.localScale.x * range, 0);

        Collider2D[] hits = Physics2D.OverlapBoxAll(position, hitBoxSize,0f,enemyLayer);
        foreach (Collider2D enemy in hits)
        {
            Debug.Log("Hit enemy");
            enemy.GetComponent<Health>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 position = (Vector2)transform.position + new Vector2(transform.localScale.x * range, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(position, hitBoxSize);
    }
}
