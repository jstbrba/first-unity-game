using UnityEngine;

namespace Game
{
    public class WalkingState : BaseState
    {
        public WalkingState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            anim.Play(WalkHash);
        }
        public override void FixedUpdate()
        {
            player.HandleMovement(1);
            player.HandleJump();
        }
    }
}
