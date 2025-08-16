using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AttackStrategy[] attacks;
    public void Attack(int index)
    {
        attacks[index].Attack(transform);
    }
    private void OnDrawGizmos()
    {
        attacks[0].DrawGizmos(transform);
    }
    public event Action OnAttackEnd;
    public void NotifyAttackEnd() => OnAttackEnd?.Invoke();
}
