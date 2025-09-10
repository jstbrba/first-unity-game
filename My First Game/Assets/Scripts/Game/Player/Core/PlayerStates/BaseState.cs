using UnityEngine;

namespace Game
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerMovement playerMovement;
        protected readonly Animator anim;

        // MOVEMENT ANIMATIONS
        protected static readonly int IdleHash = Animator.StringToHash("Idle");
        protected static readonly int WalkHash = Animator.StringToHash("Walk");
        protected static readonly int JumpHash = Animator.StringToHash("Jump");
        protected static readonly int SprintHash = Animator.StringToHash("Sprint");
        protected static readonly int CrouchHash = Animator.StringToHash("Crouch");

        // ATTACK ANIMATIONS
        protected static readonly int SwordLightHash = Animator.StringToHash("SwordLight");
        protected static readonly int SwordHeavyHash = Animator.StringToHash("SwordHeavy");
        protected static readonly int SpinAttackHash = Animator.StringToHash("SpinAttack");

        protected BaseState(PlayerMovement playerMovement, Animator anim)
        {
            this.playerMovement = playerMovement;
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
