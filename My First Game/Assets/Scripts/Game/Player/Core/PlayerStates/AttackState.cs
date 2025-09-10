using UnityEngine;

namespace Game
{
    public class AttackState : BaseState
    {
        private readonly PlayerAttack playerAttack;
        private bool attackFinished;
        public AttackState(PlayerMovement playerMovement, Animator anim, PlayerAttack playerAttack) : base(playerMovement, anim)
        {
            this.playerAttack = playerAttack;
        }
        public override void OnEnter()
        {
            anim.Play(playerAttack.currentStrategy.animHash);
            attackFinished = false;

            // attack handled in animator events

            playerAttack.OnAttackEnd += HandleAttackEnd;
        }
        public override void OnExit()
        {
            playerAttack.FinishAttack();
            playerAttack.OnAttackEnd -= HandleAttackEnd;
        }
        private void HandleAttackEnd() => attackFinished = true;
        public bool IsAttackFinished => attackFinished;
    }
}
