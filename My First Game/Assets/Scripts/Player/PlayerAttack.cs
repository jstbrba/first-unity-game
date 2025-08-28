using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public AttackStrategy[] attacks;

    private int index = 0;
    public AttackStrategy currentStrategy => attacks[index];
    private bool isAttacking;

    public void StartAttack(int index)
    {
        this.index = index;
        isAttacking = true;
    }
    public void Attack()
    {
        attacks[index].Attack(transform);
    }
    private void OnDrawGizmos()
    {
        attacks[index].DrawGizmos(transform);
    }
    public event Action OnAttackEnd;
    public void NotifyAttackEnd() => OnAttackEnd?.Invoke();
    public void FinishAttack() => isAttacking = false;
    public bool IsAttacking => isAttacking;
}
