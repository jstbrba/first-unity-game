using UnityEngine;

namespace Game
{
    public class IdleState : BaseState
    {
        public IdleState(PlayerMovement playerMovement, Animator anim) : base(playerMovement, anim) { }

        public override void OnEnter()
        {
            anim.Play(IdleHash);
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
