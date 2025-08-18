using UnityEngine;

namespace Game
{
    public class JumpState : BaseState
    {
        public JumpState(PlayerController player, Animator anim) : base(player, anim) { }

        public override void OnEnter()
        {
            anim.Play(JumpHash);
            Debug.Log("Entering Jump State");
        }
        public override void FixedUpdate()
        {
            player.HandleMovement(1);
            player.HandleJump();
        }
    }
}
