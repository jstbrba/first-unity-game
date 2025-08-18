using UnityEngine;

namespace Game
{
    public class AttackState : BaseState
    {
        private PlayerAttack playerAttack;
        private AttackStrategy currentStrategy;
        private bool attackFinished;
        public AttackState(PlayerController player, Animator anim) : base(player, anim) 
        {
            playerAttack = player.GetComponent<PlayerAttack>();
        }
        public void SetStrategy(AttackStrategy strategy) => currentStrategy = strategy;

        public override void OnEnter()
        {
            Debug.Log("Entering attack state");
            anim.Play(currentStrategy.animHash); // CAHCIHIH
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
