using UnityEngine;

namespace Game
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator anim;

        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int WalkHash = Animator.StringToHash("Walk");
        protected static readonly int WalkBackHash = Animator.StringToHash("WalkBack");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int AttackHash = Animator.StringToHash("Attack");

        protected BaseState(PlayerController player, Animator anim)
        {
            this.player = player;
            this.anim = anim;
        }

        public virtual void OnEnter()
        {
            // noop
        }
        public virtual void Update()
        {
            // noop
        }
        public virtual void FixedUpdate()
        {
            // noop
        }
        public virtual void OnExit()
        {
            // noop
        }
    }
}
