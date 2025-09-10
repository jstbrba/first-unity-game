using UnityEngine;

namespace Game
{
    public class WalkingState : BaseState
    {
        public WalkingState(PlayerMovement playerMovement, Animator anim) : base(playerMovement, anim) { }

        public override void OnEnter()
        {
            anim.Play(WalkHash);
        }
        public override void Update()
        {
            playerMovement.FlipPlayer();
        }
        public override void FixedUpdate()
        {
            playerMovement.HandleMovement(1);
            playerMovement.HandleJump();
        }
    }
}
