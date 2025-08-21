using System;
using UnityEngine;
using Utilities;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;

    [Header("Attack Attributes")]
    [SerializeField] private float damage;

    [Header("Close-range Attack")]
    [SerializeField] private Vector2 closeRangeBox;
    [SerializeField] private float closeAttackDistance;

    private CountdownTimer attackCountdown;
    [SerializeField] private float attackCoolDownTime = 3f;

    private void Start()
    {
        attackCountdown = new CountdownTimer(attackCoolDownTime);
    }
    private void Update()
    {
        attackCountdown.Tick(Time.deltaTime);
    }
    private void Attack()
    {
        Vector2 closeRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);

        Collider2D shortHit = Physics2D.OverlapBox(closeRangePos, closeRangeBox, 0, playerMask);

        if (shortHit)
        {
            shortHit.GetComponent<Health>().TakeDamage(damage);
            Debug.Log("Short range attack from enemy");
            attackCountdown.Start();
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 closeRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(closeRangePos, closeRangeBox);
    }
    public event Action OnEnemyAttackEnd;
    public void NotifyEnemyAttackEnd() => OnEnemyAttackEnd?.Invoke();
    public bool IsRunning => attackCountdown.IsRunning;
}
