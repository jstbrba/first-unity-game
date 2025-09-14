using System;
using UnityEngine;
using Utilities;

public class EnemyAttack : MonoBehaviour
{
    private IContext _context;
    private EnemyStatsModel _model;

    [SerializeField] private LayerMask playerMask;

    private int _attack;

    private CountdownTimer attackCountdown;
    [SerializeField] private float attackCoolDownTime = 3f;

    [Header("Attack Range")]
    [SerializeField] private Vector2 closeRangeBox;
    [SerializeField] private float closeAttackDistance;


    private void Start()
    {
        attackCountdown = new CountdownTimer(attackCoolDownTime);
    }
    public void Initialise(IContext context)
    {
        _context = context;

        _model = _context.ModelLocator.Get<EnemyStatsModel>();
        _attack = _model.Attack.Value;

        _model.Attack.onValueChanged += Model_Attack_OnValueChanged;
    }
    private void OnEnable()
    {
        OnEnemyAttackEnd += ResetCooldown;
    }
    private void OnDisable()
    {
        OnEnemyAttackEnd -= ResetCooldown;
    }
    private void Update()
    {
        attackCountdown.Tick(Time.deltaTime);
    }
    private void Attack()
    {
        Vector2 rangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);

        Collider2D hit = Physics2D.OverlapBox(rangePos, closeRangeBox, 0, playerMask);

        if (hit)
        {
            hit.GetComponent<IDamageable>().ApplyDamage(_attack);
        }
    }
    private void ResetCooldown() => attackCountdown.Start();

    private void OnDrawGizmos()
    {
        Vector2 closeRangePos = (Vector2)transform.position + new Vector2(transform.localScale.x * closeAttackDistance, 0);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(closeRangePos, closeRangeBox);
    }
    public event Action OnEnemyAttackEnd;
    public void NotifyEnemyAttackEnd() => OnEnemyAttackEnd?.Invoke();
    public bool IsRunning => attackCountdown.IsRunning;

    private void Model_Attack_OnValueChanged(int previous, int current) => _attack = current;
}
