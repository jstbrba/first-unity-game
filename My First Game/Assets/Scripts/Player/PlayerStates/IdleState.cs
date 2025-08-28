using UnityEngine;

namespace Game
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            anim.Play(IdleHash);
        }
        public override void FixedUpdate()
        {
            player.HandleMovement(1);
            player.HandleJump();
        }
    }
}
