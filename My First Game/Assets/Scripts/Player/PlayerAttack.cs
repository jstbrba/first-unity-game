using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public AttackStrategy[] attacks;

    [SerializeField] private int index; // make an actual way to change it in game
    public void Attack()
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
