using UnityEngine;

public abstract class AttackStrategy : ScriptableObject
{
    [SerializeField] private string animationName;
    [HideInInspector] public int animHash;
    private void OnEnable()
    {
        animHash = Animator.StringToHash(animationName);
    }
    public abstract void Attack(Transform origin);
    public virtual void DrawGizmos(Transform origin)
    {
        // noop
    }
}
