using UnityEngine;

namespace Game
{
    public class SprintState : BaseState
    {
        public SprintState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            anim.Play(SprintHash);
        }
        public override void FixedUpdate()
        {
            player.HandleMovement(1.5f);
            player.HandleJump();
        }
    }
}
