using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    [Header("Attack Attributes")]
    [SerializeField] private float damage;
    [SerializeField] private float cooldown;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Close-range Attack")]
    [SerializeField] private Vector2 closeRangeBox;
    [SerializeField] private float closeAttackDistance;

    [Header("Long-range Attack")]
    [SerializeField] private Vector2 longRangeBox;
    [SerializeField] private float longAttackDistance;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        Vector2 closeRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);
        Vector2 longRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * longAttackDistance, 0);

        Collider2D shortHit = Physics2D.OverlapBox(closeRangePos, closeRangeBox, 0, playerMask);
        Collider2D longHit = Physics2D.OverlapBox(longRangePos, longRangeBox, 0, playerMask);

        if (cooldownTimer > cooldown)
        {
            if (shortHit)
            {
                shortHit.GetComponent<Health>().TakeDamage(damage);
                cooldownTimer = 0;
                Debug.Log("Short range attack from enemy");
            }
            if (longHit)
            {
                longHit.GetComponent<Health>().TakeDamage(damage);
                cooldownTimer = 0;
                Debug.Log("Long range attack from enemy");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 closeRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);
        Vector2 longRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * longAttackDistance, 0);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(closeRangePos, closeRangeBox);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube (longRangePos, longRangeBox);
    }
}
