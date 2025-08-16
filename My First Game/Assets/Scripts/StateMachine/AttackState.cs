using UnityEngine;

namespace Game
{
    public class AttackState : BaseState
    {
        private PlayerAttack playerAttack;
        private bool attackFinished;
        public AttackState(PlayerController player, Animator anim) : base(player, anim) 
        {
            playerAttack = player.GetComponent<PlayerAttack>();
        }

        public override void OnEnter()
        {
            anim.Play(AttackHash);
            attackFinished = false;

            // attack handled in animator events

            playerAttack.OnAttackEnd += HandleAttackEnd;
        }
        public override void OnExit()
        {
            playerAttack.OnAttackEnd -= HandleAttackEnd;
        }
        private void HandleAttackEnd() => attackFinished = true;
        public bool IsAttackFinished => attackFinished;
    }
}
