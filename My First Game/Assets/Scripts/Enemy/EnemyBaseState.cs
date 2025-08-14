using UnityEngine;

namespace Game
{
    public abstract class EnemyBaseState : IState
    {
        protected readonly Enemy enemy;
        protected readonly Animator anim;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int WalkHash = Animator.StringToHash("Walk");
        protected static readonly int WalkBackHash = Animator.StringToHash("WalkBack");

        protected EnemyBaseState(Enemy enemy, Animator anim)
        {
            this.enemy = enemy;
            this.anim = anim;
        }
        public virtual void FixedUpdate()
        {
            // noop
        }

        public virtual void OnEnter()
        {
            // noop
        }

        public virtual void OnExit()
        {
            // noop
        }

        public virtual void Update()
        {
            // noop
        }
    }
}
