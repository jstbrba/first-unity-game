using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] public AttackStrategy[] attacks;

    private int index = 0;
    public AttackStrategy currentStrategy => attacks[index];

    // TODO : Decouple player attack and HUD
    private void OnEnable()
    {
        HeadsUpDisplay.OnButtonPressed += SelectStrategy;
    }
    private void OnDisable()
    {
        HeadsUpDisplay.OnButtonPressed -= SelectStrategy;
    }

    private void SelectStrategy(int i)
    {
        if (i < attacks.Length)
        {
            index = i;
        }
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
}
