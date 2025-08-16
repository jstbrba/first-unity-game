using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    public abstract void Attack(Transform origin);
    public virtual void DrawGizmos(Transform origin)
    {
        // noop
    }
}
