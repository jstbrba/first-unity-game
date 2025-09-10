using UnityEngine;

namespace Game
{
    public class CrouchState : BaseState
    {
        public CrouchState(PlayerMovement playerMovement, Animator anim) : base(playerMovement, anim) { }

        public override void OnEnter()
        {
            anim.Play(CrouchHash);
        }
        public override void Update()
        {
            playerMovement.FlipPlayer();
        }
    }
}
