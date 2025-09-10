using UnityEngine;

namespace Game
{
    public class SprintState : BaseState
    {
        public SprintState(PlayerMovement playerMovement, Animator anim) : base(playerMovement, anim) { }

        public override void OnEnter()
        {
            anim.Play(SprintHash);
        }
        public override void Update()
        {
            playerMovement.FlipPlayer();
        }
        public override void FixedUpdate()
        {
            playerMovement.HandleMovement(1.5f);
            playerMovement.HandleJump();
        }
    }
}
